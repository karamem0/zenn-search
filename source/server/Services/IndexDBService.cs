//
// Copyright (c) 2023 karamem0
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

namespace Karamem0.ZennSearch.Services
{

    public class IndexDBService
    {

        private readonly IMongoCollection<IndexData> collection;

        public IndexDBService(MongoClient client, string databaseName, string collectionName)
        {
            this.collection = client
                .GetDatabase(databaseName)
                .GetCollection<IndexData>(collectionName);
        }

        public async Task<IndexData?> FindOneAsync(string name, string etag)
        {
            return await this.collection
                .FindAsync(
                    Builders<IndexData>.Filter.And(
                        Builders<IndexData>.Filter.Eq("_id", name),
                        Builders<IndexData>.Filter.Eq("etag", etag)
                    )
                )
                .ContinueWith(_ => _.Result.FirstOrDefault());
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

        public async Task<IEnumerable<SearchIndexData?>> SearchAsync(IReadOnlyCollection<float> vector, int count)
        {
            return await this.collection
                .Aggregate(PipelineDefinition<IndexData, SearchIndexData>.Create(
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
                ))
                .ToListAsync()
                ?? Enumerable.Empty<SearchIndexData?>();
        }

    }

}
