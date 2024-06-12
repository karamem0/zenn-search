//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Logging;
using Karamem0.ZennSearch.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Tasks;

public class DeleteTask(
    ILoggerFactory loggerFactory,
    AzureAISearchService azureAISearchService,
    AzureBlobStorageService azureBlobStorageService,
    AzureMongoDBService azureMongoDBService
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<DeleteTask>();

    private readonly AzureAISearchService azureAISearchService = azureAISearchService;

    private readonly AzureBlobStorageService azureBlobStorageService = azureBlobStorageService;

    private readonly AzureMongoDBService azureMongoDBService = azureMongoDBService;

    public async Task DeleteAISearch()
    {
        try
        {
            this.logger.DeletionStarted();
            await foreach (var blobName in this.azureBlobStorageService.GetDeletedBlobNamesAsync())
            {
                if (blobName is null)
                {
                    continue;
                }
                await this.azureAISearchService.DeleteOneAsync(blobName);
            }
        }
        finally
        {
            this.logger.DeletionEnded();
        }
    }

    public async Task DeleteMongoDB()
    {
        try
        {
            this.logger.DeletionStarted();
            await foreach (var blobName in this.azureBlobStorageService.GetDeletedBlobNamesAsync())
            {
                if (blobName is null)
                {
                    continue;
                }
                await this.azureMongoDBService.DeleteOneAsync(blobName);
            }
        }
        finally
        {
            this.logger.DeletionEnded();
        }
    }

}
