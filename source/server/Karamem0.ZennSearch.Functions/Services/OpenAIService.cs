//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Azure.AI.OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Services
{

    public class OpenAIService
    {

        private readonly OpenAIClient client;

        private readonly string modelName;

        public OpenAIService(OpenAIClient client, string modelName)
        {
            this.client = client;
            this.modelName = modelName;
        }

        public async Task<float[]> GetEmbeddingsAsync(string text)
        {
            return await this.client
                .GetEmbeddingsAsync(new EmbeddingsOptions()
                {
                    DeploymentName = this.modelName,
                    Input = new List<string>()
                    {
                        text
                    }
                })
                .ContinueWith(_ => _.Result.Value.Data[0].Embedding.ToArray());
        }


    }

}
