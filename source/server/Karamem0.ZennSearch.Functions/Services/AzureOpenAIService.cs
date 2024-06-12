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

namespace Karamem0.ZennSearch.Services;

public class AzureOpenAIService(AzureOpenAIClient client, string modelName)
{

    private readonly AzureOpenAIClient client = client;

    private readonly string modelName = modelName;

    public async Task<float[]> GetEmbeddingsAsync(string text)
    {
        return await this.client
            .GetEmbeddingClient(this.modelName)
            .GenerateEmbeddingAsync(text)
            .ContinueWith((task) => task.Result.Value.Vector.ToArray());
    }

}
