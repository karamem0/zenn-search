//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Helpers;
using Karamem0.ZennSearch.Logging;
using Karamem0.ZennSearch.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Karamem0.ZennSearch.Functions
{

    public class SearchFunction
    {

        private readonly ILogger logger;

        private readonly IndexDBService indexDBService;

        private readonly OpenAIService openAIService;

        public SearchFunction(
            ILoggerFactory loggerFactory,
            IndexDBService indexDBService,
            OpenAIService openAIService
        )
        {
            this.logger = loggerFactory.CreateLogger<SearchFunction>();
            this.indexDBService = indexDBService;
            this.openAIService = openAIService;
        }

        [Function("Search")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "GET")] HttpRequestData req)
        {
            var res = req.CreateResponse();
            try
            {
                this.logger.SearchStarted();
                var queries = HttpUtility.ParseQueryString(req.Url.Query);
                if (queries == null)
                {
                    res.StatusCode = HttpStatusCode.BadRequest;
                    return res;
                }
                if (string.IsNullOrEmpty(queries["query"]))
                {
                    res.StatusCode = HttpStatusCode.BadRequest;
                    return res;
                }
                var vector = await this.openAIService.GetEmbeddingsAsync(queries["query"] ?? "");
                var count = UInt32Parser.Parse(queries["count"], 10);
                var results = this.indexDBService.SearchAsync(vector, (int)count);
                res.StatusCode = HttpStatusCode.OK;
                await res.WriteAsJsonAsync(new { value = results });
                return res;
            }
            catch (Exception ex)
            {
                this.logger.UnhandledError(ex);
                res.StatusCode = HttpStatusCode.InternalServerError;
                return res;
            }
            finally
            {
                this.logger.SearchEnded();
            }
        }

    }

}
