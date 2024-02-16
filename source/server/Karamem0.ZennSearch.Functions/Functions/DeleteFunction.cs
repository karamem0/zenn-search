//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Logging;
using Karamem0.ZennSearch.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Functions
{

    public class DeleteFunction(
        ILoggerFactory loggerFactory,
        BlobStorageService blobStorageService,
        IndexDBService indexDBService
    )
    {

        private readonly ILogger logger = loggerFactory.CreateLogger<DeleteFunction>();

        private readonly BlobStorageService blobStorageService = blobStorageService;

        private readonly IndexDBService indexDBService = indexDBService;

#pragma warning disable IDE0060

        [Function("Delete")]
        public async Task Run([TimerTrigger("0 0 0 * * *")] object timerInfo)
        {
            try
            {
                this.logger.DeletionStarted();
                await foreach (var mongoItem in this.indexDBService.FindAllAsync())
                {
                    if (
                        mongoItem is null ||
                        mongoItem.Id is null
                    )
                    {
                        continue;
                    }
                    if (await this.blobStorageService.ExistsAsync(Path.ChangeExtension(mongoItem.Id, ".md")))
                    {
                        continue;
                    }
                    await this.indexDBService.DeleteOneAsync(mongoItem.Id);
                }
            }
            catch (Exception ex)
            {
                this.logger.UnhandledError(ex);
            }
            finally
            {
                this.logger.DeletionEnded();
            }
        }

#pragma warning restore IDE0060

    }

}
