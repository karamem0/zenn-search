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

namespace Karamem0.ZennSearch.Functions;

public class DeleteFunction(
    ILoggerFactory loggerFactory,
    DeleteTask deleteTask
)
{

    private readonly ILogger logger = loggerFactory.CreateLogger<DeleteFunction>();

    private readonly DeleteTask deleteTask = deleteTask;

#pragma warning disable IDE0060

    [Function("Delete")]
    public async Task Run([TimerTrigger("0 0 0 * * *")] object timerInfo)
    {
        try
        {
            await Task.WhenAll(
                this.deleteTask.DeleteAISearch(),
                this.deleteTask.DeleteMongoDB()
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
