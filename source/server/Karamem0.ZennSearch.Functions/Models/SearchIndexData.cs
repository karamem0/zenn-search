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

namespace Karamem0.ZennSearch.Models
{

    public class SearchIndexData
    {

        [BsonElement("@@score")]
        [JsonPropertyName("@@score")]
        public double? Score { get; set; }

        [BsonElement("_id")]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [BsonElement("value")]
        [JsonPropertyName("value")]
        public IndexData? Value { get; set; }

    }

}
