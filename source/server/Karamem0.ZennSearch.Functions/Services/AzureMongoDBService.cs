//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Services;

public class AzureMongoDBService(
    MongoClient client,
    string databaseName,
    string collectionName
)
{

    private readonly IMongoCollection<IndexData> collection = client
            .GetDatabase(databaseName)
            .GetCollection<IndexData>(collectionName);

    public async Task DeleteOneAsync(string name)
    {
        _ = await this.collection
            .DeleteOneAsync(
                Builders<IndexData>.Filter.Eq("_id", name)
            );
    }

    public async Task<IndexData?> FindOneAsync(string name, string etag)
    {
        var cursor = await this.collection
            .FindAsync(
                Builders<IndexData>.Filter.And(
                    Builders<IndexData>.Filter.Eq("_id", name),
                    Builders<IndexData>.Filter.Eq("etag", etag)
                )
            );
        return await cursor.FirstOrDefaultAsync();
    }

    public async IAsyncEnumerable<IndexData> FindAllAsync()
    {
        var cursor = await this.collection
            .FindAsync(Builders<IndexData>.Filter.Empty);
        while (await cursor.MoveNextAsync())
        {
            foreach (var value in cursor.Current)
            {
                yield return value;
            }
        }
    }

    public async Task ReplaceOneAsync(IndexData value)
    {
        _ = await this.collection
            .ReplaceOneAsync(
                Builders<IndexData>.Filter.And(
                    Builders<IndexData>.Filter.Eq("_id", value.Id)
                ),
                value,
                new ReplaceOptions()
                {
                    IsUpsert = true
                }
            );
    }

    public async IAsyncEnumerable<SearchIndexData> SearchAsync(float[] vector, int count)
    {
        var cursor = await this.collection
            .AggregateAsync(PipelineDefinition<IndexData, SearchIndexData>.Create(
                new BsonDocument()
                {
                    ["$search"] = new BsonDocument()
                    {
                        ["cosmosSearch"] = new BsonDocument()
                        {
                            ["vector"] = new BsonArray(vector),
                            ["path"] = "contentVector",
                            ["k"] = count
                        },
                        ["returnStoredSource"] = true
                    }
                },
                new BsonDocument()
                {
                    ["$project"] = new BsonDocument()
                    {
                        ["@@score"] = new BsonDocument()
                        {
                            ["$meta"] = "searchScore"
                        },
                        ["value"] = "$$ROOT"
                    }
                }
            ));
        while (await cursor.MoveNextAsync())
        {
            foreach (var value in cursor.Current)
            {
                yield return value;
            }
        }
    }

}
