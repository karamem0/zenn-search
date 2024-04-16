//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Helpers;

public static class RerankHelper
{

    public static SearchIndexData[] Fuse(IEnumerable<SearchIndexData[]> rankings, double k = 60)
    {
        var results = new List<SearchIndexData>();
        foreach (var ranking in rankings)
        {
            var ordered = ranking.OrderByDescending(item => item.Score).Select((value, order) => Tuple.Create(value, order + 1.0));
            foreach (var (value, order) in ordered)
            {
                var index = results.FindIndex(item => item.Id == value.Id);
                if (index < 0)
                {
                    value.Score = 1.0 / (order + k);
                    results.Add(value);
                }
                else
                {
                    results[index].Score = results[index].Score + (1.0 / (order + k));
                }
            }
        }
        return [.. results.OrderByDescending(item => item.Score)];
    }

}
