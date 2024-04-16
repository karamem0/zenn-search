//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Logging;
using Karamem0.ZennSearch.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Functions.MongoDB;

public class UploadFunction(
    ILoggerFactory loggerFactory,
    UploadTask uploadTask
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<UploadFunction>();

    private readonly UploadTask uploadTask = uploadTask;

#pragma warning disable IDE0060

    [Function("Upload")]
    public async Task Run([TimerTrigger("0 0 * * * *")] object timerInfo)
    {
        try
        {
            await Task.WhenAll(
                this.uploadTask.UploadAISearch(),
                this.uploadTask.UploadMongoDB()
            );
        }
        catch (Exception ex)
        {
            this.logger.UnhandledError(ex.Message, ex);
            throw;
        }
    }

#pragma warning restore IDE0060

}
