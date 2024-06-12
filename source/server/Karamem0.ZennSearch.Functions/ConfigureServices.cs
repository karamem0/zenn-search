//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using AutoMapper;
using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using Azure.Search.Documents;
using Azure.Storage.Blobs;
using Karamem0.ZennSearch.Services;
using Karamem0.ZennSearch.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch;

public static class ConfigureServices
{

    public static readonly DefaultAzureCredential DefaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions()
    {
        ExcludeVisualStudioCodeCredential = true
    });

    public static IServiceCollection AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var azureBlobStorageUrl = configuration.GetValue<string>("AzureBlobStorageUrl") ?? throw new InvalidOperationException();
        _ = services.AddSingleton(_ => new BlobContainerClient(new Uri(azureBlobStorageUrl), DefaultAzureCredential));
        return services;
    }

    public static IServiceCollection AddAISearch(this IServiceCollection services, IConfiguration configuration)
    {
        var azureAISearchKey = configuration.GetValue<string>("AzureAISearchKey") ?? throw new InvalidOperationException();
        var azureAISearchUrl = configuration.GetValue<string>("AzureAISearchUrl") ?? throw new InvalidOperationException();
        var azureAISearchIndexName = configuration.GetValue<string>("AzureAISearchIndexName") ?? throw new InvalidOperationException();
        _ = services.AddSingleton(_ => new SearchClient(new Uri(azureAISearchUrl), azureAISearchIndexName, new AzureKeyCredential(azureAISearchKey)));
        return services;
    }

    public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
    {
        var azureMongoDBServerName = configuration.GetValue<string>("AzureMongoDBServerName") ?? throw new InvalidOperationException();
        var azureMongoDBUserName = configuration.GetValue<string>("AzureMongoDBUserName") ?? throw new InvalidOperationException();
        var azureMongoDBPassword = configuration.GetValue<string>("AzureMongoDBPassword") ?? throw new InvalidOperationException();
        _ = services.AddSingleton(_ => new MongoClient(new MongoClientSettings()
        {
            Credential = new MongoCredential(
                "SCRAM-SHA-256",
                new MongoInternalIdentity("admin", azureMongoDBUserName),
                new PasswordEvidence(azureMongoDBPassword)
            ),
            Scheme = ConnectionStringScheme.MongoDBPlusSrv,
            Server = new MongoServerAddress(azureMongoDBServerName),
            UseTls = true,
        }));
        return services;
    }

    public static IServiceCollection AddOpenAI(this IServiceCollection services, IConfiguration configuration)
    {
        var azureOpenAIUrl = configuration.GetValue<string>("AzureOpenAIUrl") ?? throw new InvalidOperationException();
        _ = services.AddSingleton(_ => new AzureOpenAIClient(new Uri(azureOpenAIUrl), DefaultAzureCredential));
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var azureMongoDBDatabaseName = configuration.GetValue<string>("AzureMongoDBDatabaseName") ?? throw new InvalidOperationException();
        var azureMongoDBCollectionName = configuration.GetValue<string>("AzureMongoDBCollectionName") ?? throw new InvalidOperationException();
        var azureOpenAIModelName = configuration.GetValue<string>("AzureOpenAIModelName") ?? throw new InvalidOperationException();
        _ = services.AddScoped(provider => new AzureAISearchService(
            provider.GetService<SearchClient>() ?? throw new InvalidOperationException(),
            provider.GetService<IMapper>() ?? throw new InvalidOperationException()
        ));
        _ = services.AddScoped(provider => new AzureBlobStorageService(
            provider.GetService<BlobContainerClient>() ?? throw new InvalidOperationException()
        ));
        _ = services.AddScoped(provider => new AzureMongoDBService(
            provider.GetService<MongoClient>() ?? throw new InvalidOperationException(),
            azureMongoDBDatabaseName,
            azureMongoDBCollectionName
        ));
        _ = services.AddScoped(provider => new AzureOpenAIService(
            provider.GetService<AzureOpenAIClient>() ?? throw new InvalidOperationException(),
            azureOpenAIModelName
        ));
        return services;
    }

    public static IServiceCollection AddTasks(this IServiceCollection services)
    {
        _ = services.AddScoped<DeleteTask>();
        _ = services.AddScoped<SearchTask>();
        _ = services.AddScoped<UploadTask>();
        return services;
    }

}
