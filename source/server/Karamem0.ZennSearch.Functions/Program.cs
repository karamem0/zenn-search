//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

#pragma warning disable CA1852

using Karamem0.ZennSearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

var builder = new HostBuilder();
_ = builder.ConfigureFunctionsWorkerDefaults();
_ = builder.ConfigureAppConfiguration((context, builder) =>
{
    _ = builder.AddJsonFile("appsettings.json", true, true);
    _ = builder.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT")}.json", true, true);
    _ = builder.AddUserSecrets<Program>(true, true);
    _ = builder.AddEnvironmentVariables();
});
_ = builder.ConfigureServices((context, services) =>
{
    var configuration = context.Configuration;
    _ = services.AddApplicationInsightsTelemetryWorkerService();
    _ = services.AddLogging(builder => builder.AddApplicationInsights());
    _ = services.AddHttpClient();
    _ = services.AddBlobStorage(configuration);
    _ = services.AddIndexDB(configuration);
    _ = services.AddOpenAI(configuration);
    _ = services.AddServices(configuration);
});

var host = builder.Build();

host.Run();

#pragma warning restore CA1852
