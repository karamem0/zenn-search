//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//
using AutoMapper;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Karamem0.ZennSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Services;

public class AzureAISearchService(SearchClient client, IMapper mapper)
{

    private readonly SearchClient client = client;

    private readonly IMapper mapper = mapper;

    public async Task DeleteOneAsync(string name)
    {
        _ = await this.client.DeleteDocumentsAsync("id", [name]);
    }

    public async Task<IndexData?> FindOneAsync(string name, string etag)
    {
        var options = new SearchOptions()
        {
            Filter = $"id eq '{name}' and etag eq '{etag}'",
            QueryType = SearchQueryType.Full
        };
        var response = await this.client.SearchAsync<IndexData>("*", options);
        var results = response.Value.GetResults().Select(item => item.Document).ToArray();
        return results.FirstOrDefault();
    }

    public async IAsyncEnumerable<IndexData> FindAllAsync()
    {
        var options = new SearchOptions()
        {
            QueryType = SearchQueryType.Full
        };
        var response = await this.client.SearchAsync<IndexData>("*", options);
        var results = response.Value.GetResultsAsync();
        await foreach (var page in results.AsPages())
        {
            foreach (var value in page.Values)
            {
                yield return value.Document;
            }
        }
    }

    public async Task ReplaceOneAsync(IndexData value)
    {
        _ = await this.client.UploadDocumentsAsync([value]);
    }

    public async IAsyncEnumerable<SearchIndexData> SearchAsync(float[] vector, int count)
    {
        var vectorQuery = new VectorizedQuery(new ReadOnlyMemory<float>(vector))
        {
            KNearestNeighborsCount = count
        };
        vectorQuery.Fields.Add("contentVector");
        var vectorOptions = new VectorSearchOptions();
        vectorOptions.Queries.Add(vectorQuery);
        var searchOptions = new SearchOptions()
        {
            VectorSearch = vectorOptions
        };
        var response = await this.client.SearchAsync<IndexData>(searchOptions);
        var results = response.Value.GetResultsAsync();
        await foreach (var page in results.AsPages())
        {
            foreach (var value in page.Values)
            {
                yield return this.mapper.Map<SearchIndexData>(value.Document);
            }
        }
    }

}
