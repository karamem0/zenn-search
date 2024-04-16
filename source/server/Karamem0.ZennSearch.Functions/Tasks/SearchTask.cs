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

public class SearchTask(
    ILoggerFactory loggerFactory,
    AzureAISearchService azureAISearchService,
    AzureMongoDBService azureMongoDBService,
    AzureOpenAIService azureOpenAIService
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<SearchTask>();

    private readonly AzureAISearchService azureAISearchService = azureAISearchService;

    private readonly AzureMongoDBService azureMongoDBService = azureMongoDBService;

    private readonly AzureOpenAIService azureOpenAIService = azureOpenAIService;

    public async Task<SearchIndexData[]> SearchAISearch(string query, int count)
    {
        try
        {
            this.logger.SearchStarted(query, count);
            var vector = await this.azureOpenAIService.GetEmbeddingsAsync(query);
            var results = this.azureAISearchService.SearchAsync(vector, count);
            return await results.ToArrayAsync();
        }
        finally
        {
            this.logger.SearchEnded();
        }
    }

    public async Task<SearchIndexData[]> SearchMongoDB(string query, int count)
    {
        try
        {
            this.logger.SearchStarted(query, count);
            var vector = await this.azureOpenAIService.GetEmbeddingsAsync(query);
            var results = this.azureMongoDBService.SearchAsync(vector, count);
            return await results.ToArrayAsync();
        }
        finally
        {
            this.logger.SearchEnded();
        }
    }

}
