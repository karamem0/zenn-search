//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Azure.AI.OpenAI;
using Azure.Identity;
using Azure.Storage.Blobs;
using Karamem0.ZennSearch.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch
{

    public static class ConfigureServices
    {

        public static IServiceCollection AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var dataSourceStorageUrl = configuration.GetValue<string>("AzureDataSourceStorageUrl") ?? throw new InvalidOperationException();
            _ = services.AddSingleton(_ => new BlobContainerClient(new Uri(dataSourceStorageUrl), new DefaultAzureCredential()));
            return services;
        }

        public static IServiceCollection AddIndexDB(this IServiceCollection services, IConfiguration configuration)
        {
            var indexDBServerName = configuration.GetValue<string>("AzureIndexDBServerName") ?? throw new InvalidOperationException();
            var indexDBUserName = configuration.GetValue<string>("AzureIndexDBUserName") ?? throw new InvalidOperationException();
            var indexDBPassword = configuration.GetValue<string>("AzureIndexDBPassword") ?? throw new InvalidOperationException();
            _ = services.AddSingleton(_ => new MongoClient(new MongoClientSettings()
            {
                Credential = new MongoCredential(
                    "SCRAM-SHA-256",
                    new MongoInternalIdentity("admin", indexDBUserName),
                    new PasswordEvidence(indexDBPassword)
                ),
                Scheme = ConnectionStringScheme.MongoDBPlusSrv,
                Server = new MongoServerAddress(indexDBServerName),
                UseTls = true,
            }));
            return services;
        }

        public static IServiceCollection AddOpenAI(this IServiceCollection services, IConfiguration configuration)
        {
            var openAIEndpointUrl = configuration.GetValue<string>("AzureOpenAIEndpointUrl") ?? throw new InvalidOperationException();
            _ = services.AddSingleton(_ => new OpenAIClient(new Uri(openAIEndpointUrl), new DefaultAzureCredential()));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var indexDBDatabaseName = configuration.GetValue<string>("AzureIndexDBDatabaseName") ?? throw new InvalidOperationException();
            var indexDBCollectionName = configuration.GetValue<string>("AzureIndexDBCollectionName") ?? throw new InvalidOperationException();
            var openAIModelName = configuration.GetValue<string>("AzureOpenAIModelName") ?? throw new InvalidOperationException();
            _ = services.AddScoped(provider => new BlobStorageService(
                provider.GetService<BlobContainerClient>() ?? throw new InvalidOperationException()
            ));
            _ = services.AddScoped(provider => new IndexDBService(
                provider.GetService<MongoClient>() ?? throw new InvalidOperationException(),
                indexDBDatabaseName,
                indexDBCollectionName
            ));
            _ = services.AddScoped(provider => new OpenAIService(
                provider.GetService<OpenAIClient>() ?? throw new InvalidOperationException(),
                openAIModelName
            ));
            return services;
        }

    }

}
