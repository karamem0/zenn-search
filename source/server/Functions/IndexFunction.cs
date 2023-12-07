//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Logging;
using Karamem0.ZennSearch.Models;
using Karamem0.ZennSearch.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Functions
{

    public class IndexFunction
    {

        private readonly ILogger logger;

        private readonly BlobStorageService blobStorageService;

        private readonly IndexDBService indexDBService;

        private readonly OpenAIService openAIService;

        public IndexFunction(
            ILoggerFactory loggerFactory,
            BlobStorageService blobStorageService,
            IndexDBService indexDBService,
            OpenAIService openAIService
        )
        {
            this.logger = loggerFactory.CreateLogger<IndexFunction>();
            this.blobStorageService = blobStorageService;
            this.indexDBService = indexDBService;
            this.openAIService = openAIService;
        }

#pragma warning disable IDE0060

        [Function("Index")]
        public async Task Run([TimerTrigger("0 0 * * * *")] object timerInfo)
        {
            try
            {
                this.logger.IndexStarted();
                await foreach (var blobItem in this.blobStorageService.GetBlobsAsync())
                {
                    if (blobItem.Name is null ||
                        blobItem.Content is null ||
                        blobItem.ETag is null
                    )
                    {
                        continue;
                    }
                    var mongoItem = await this.indexDBService.FindOneAsync(blobItem.Name, blobItem.ETag);
                    if (mongoItem is null)
                    {
                        var vector = await this.openAIService.GetEmbeddingsAsync(blobItem.Content);
                        await this.indexDBService.ReplaceOneAsync(
                            new IndexData()
                            {
                                Id = blobItem.Name,
                                Title = blobItem.Title,
                                Emoji = blobItem.Emoji,
                                Content = blobItem.Content,
                                ContentVector = vector,
                                Created = blobItem.Created,
                                Updated = blobItem.Updated,
                                ETag = blobItem.ETag
                            }
                        );
                        this.logger.IndexExecuted(blobItem.Name, blobItem.ETag);
                    }
                    else
                    {
                        this.logger.IndexSkipped(blobItem.Name, blobItem.ETag);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.UnhandledError(ex);
            }
            finally
            {
                this.logger.IndexEnded();
            }
        }

#pragma warning restore IDE0060

    }

}
