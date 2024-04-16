//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Karamem0.ZennSearch.Models;

public class SearchRequestData
{

    public SearchRequestData(string queryString)
    {
        var queries = HttpUtility.ParseQueryString(queryString);
        this.Target = EnumParser.Parse(queries["target"], SearchTarget.Both);
        this.Query = queries["query"] ?? throw new InvalidOperationException();
        this.Count = (int)UInt32Parser.Parse(queries["count"], 10);
    }

    public SearchTarget Target { get; set; }

    public string Query { get; set; }

    public int Count { get; set; }

}
