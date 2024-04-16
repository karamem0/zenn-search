//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Karamem0.ZennSearch.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Helpers.Tests;

public class RerankHelperTests
{

    [Test()]
    public void Fuse()
    {
        var rankings = new[]
        {
            new[]
            {
                new SearchIndexData()
                {
                    Score = 0.8,
                    Id = "ほげほげ"
                },
                new SearchIndexData()
                {
                    Score = 0.6,
                    Id = "ふがふが"
                },
                new SearchIndexData()
                {
                    Score = 0.4,
                    Id = "ぴよぴよ"
                },
            },
            new[]
            {
                new SearchIndexData()
                {
                    Score = 0.7,
                    Id = "ふがふが"
                },
                new SearchIndexData()
                {
                    Score = 0.5,
                    Id = "ほげほげ"
                },
                new SearchIndexData()
                {
                    Score = 0.3,
                    Id = "ぴよぴよ"
                },
            },
            new[]
            {
                new SearchIndexData()
                {
                    Score = 0.9,
                    Id = "ぴよぴよ"
                },
                new SearchIndexData()
                {
                    Score = 0.2,
                    Id = "ほげほげ"
                },
                new SearchIndexData()
                {
                    Score = 0.1,
                    Id = "ふがふが"
                }
            }
        };
        var expected = new[]
        {
            new SearchIndexData()
            {
                Score = (1.0 / 1) + (1.0 / 2) + (1.0 / 2),
                Id = "ほげほげ"
            },
            new SearchIndexData()
            {
                Score = (1.0 / 2) + (1.0 / 1) + (1.0 / 3),
                Id = "ふがふが"
            },
            new SearchIndexData()
            {
                Score = (1.0 / 3) + (1.0 / 3) + (1.0 / 1),
                Id = "ぴよぴよ"
            }
        };
        var actual = RerankHelper.Fuse(rankings, 0.0);
        Assert.That(actual, Is.EqualTo(expected).AsCollection.UsingPropertiesComparer());
    }

}
