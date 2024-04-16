//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Helpers;
using Karamem0.ZennSearch.Logging;
using Karamem0.ZennSearch.Models;
using Karamem0.ZennSearch.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Functions.AISearch;

public class SearchFunction(
    ILoggerFactory loggerFactory,
    SearchTask searchTask
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<SearchFunction>();

    private readonly SearchTask searchTask = searchTask;

    [Function("Search")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequestData httpRequest)
    {
        var httpResponse = httpRequest.CreateResponse();
        try
        {
            var searchRequest = new SearchRequestData(httpRequest.Url.Query);
            var searchResults = await Task.WhenAll(
                this.searchTask.SearchAISearch(searchRequest.Query, searchRequest.Count),
                this.searchTask.SearchMongoDB(searchRequest.Query, searchRequest.Count)
            );
            var searchResponse = new SearchResponseData()
            {
                Value = searchRequest.Target switch
                {
                    SearchTarget.Both => RerankHelper.Fuse(searchResults).Take(searchRequest.Count).ToArray(),
                    SearchTarget.AISearch => searchResults[0],
                    SearchTarget.MongoDB => searchResults[1],
                    _ => throw new NotImplementedException()
                }
            };
            await httpResponse.WriteAsJsonAsync(searchResponse);
            httpResponse.StatusCode = HttpStatusCode.OK;
            return httpResponse;
        }
        catch (InvalidOperationException ex)
        {
            this.logger.UnhandledError(ex.Message, ex);
            httpResponse.StatusCode = HttpStatusCode.BadRequest;
            return httpResponse;
        }
        catch (Exception ex)
        {
            this.logger.UnhandledError(ex.Message, ex);
            httpResponse.StatusCode = HttpStatusCode.InternalServerError;
            return httpResponse;
        }
        finally
        {
            this.logger.SearchEnded();
        }
    }

}
