//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Logging;
using Karamem0.ZennSearch.Models;
using Karamem0.ZennSearch.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Tasks;

public class UploadTask(
    ILoggerFactory loggerFactory,
    AzureAISearchService azureAISearchService,
    AzureBlobStorageService azureBlobStorageService,
    AzureMongoDBService azureMongoDBService,
    AzureOpenAIService azureOpenAIService
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<UploadTask>();

    private readonly AzureAISearchService azureAISearchService = azureAISearchService;

    private readonly AzureBlobStorageService azureBlobStorageService = azureBlobStorageService;

    private readonly AzureMongoDBService azureMongoDBService = azureMongoDBService;

    private readonly AzureOpenAIService azureOpenAIService = azureOpenAIService;

    public async Task UploadAISearch()
    {
        try
        {
            this.logger.CreationStarted();
            await foreach (var blobItem in this.azureBlobStorageService.GetBlobsAsync())
            {
                if (blobItem.Name is null)
                {
                    continue;
                }
                if (blobItem.Content is null)
                {
                    continue;
                }
                if (blobItem.ETag is null)
                {
                    continue;
                }
                var indexItem = await this.azureAISearchService.FindOneAsync(blobItem.Name, blobItem.ETag);
                if (indexItem is null)
                {
                    var vector = await this.azureOpenAIService.GetEmbeddingsAsync(blobItem.Content);
                    await this.azureAISearchService.ReplaceOneAsync(
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
                    this.logger.CreationExecuted(blobItem.Name, blobItem.ETag);
                }
                else
                {
                    this.logger.CreationSkipped(blobItem.Name, blobItem.ETag);
                }
            }
        }
        finally
        {
            this.logger.CreationEnded();
        }
    }

    public async Task UploadMongoDB()
    {
        try
        {
            this.logger.CreationStarted();
            await foreach (var blobItem in this.azureBlobStorageService.GetBlobsAsync())
            {
                if (blobItem.Name is null)
                {
                    continue;
                }
                if (blobItem.Content is null)
                {
                    continue;
                }
                if (blobItem.ETag is null)
                {
                    continue;
                }
                var indexItem = await this.azureMongoDBService.FindOneAsync(blobItem.Name, blobItem.ETag);
                if (indexItem is null)
                {
                    var vector = await this.azureOpenAIService.GetEmbeddingsAsync(blobItem.Content);
                    await this.azureMongoDBService.ReplaceOneAsync(
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
                    this.logger.CreationExecuted(blobItem.Name, blobItem.ETag);
                }
                else
                {
                    this.logger.CreationSkipped(blobItem.Name, blobItem.ETag);
                }
            }
        }
        finally
        {
            this.logger.CreationEnded();
        }
    }

}
