//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Models;

public class IndexData
{

    [BsonIgnore()]
    [JsonPropertyName("@search.score")]
    public double? Score { get; set; }

    [BsonElement("_id")]
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [BsonElement("title")]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [BsonElement("emoji")]
    [JsonPropertyName("emoji")]
    public string? Emoji { get; set; }

    [BsonElement("content")]
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [BsonElement("contentVector")]
    [JsonPropertyName("contentVector")]
    public float[]? ContentVector { get; set; }

    [BsonElement("created")]
    [JsonPropertyName("created")]
    public DateTime? Created { get; set; }

    [BsonElement("updated")]
    [JsonPropertyName("updated")]
    public DateTime? Updated { get; set; }

    [BsonElement("etag")]
    [JsonPropertyName("etag")]
    public string? ETag { get; set; }

}
