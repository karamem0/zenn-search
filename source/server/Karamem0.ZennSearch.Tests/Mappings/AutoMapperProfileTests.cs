//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using AutoMapper;
using Karamem0.ZennSearch.Models;
using Karamem0.ZennSearch.Models.Mappings;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Helpers.Tests;

public class AutoMapperProfileTests
{

    [Test()]
    public void Map_IndexDataToSearchIndexData()
    {
        var profile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(config => config.AddProfile(profile));
        var mapper = new Mapper(configuration);
        var source = new IndexData()
        {
            Score = 1.0,
            Id = "aa049c8d-ac12-45e2-a86d-ee90bfed5dd5",
            Title = "ほげほげ",
            Emoji = "🥺",
            Content = "ふがふが",
            Created = new DateTime(2020, 1, 1, 9, 0, 0, DateTimeKind.Utc),
            Updated = new DateTime(2022, 1, 1, 9, 0, 0, DateTimeKind.Utc),
            ETag = "9377e467-707f-4556-8829-1600a8442684"
        };
        var expected = new SearchIndexData()
        {
            Score = 1.0,
            Id = "aa049c8d-ac12-45e2-a86d-ee90bfed5dd5"
        };
        var actual = mapper.Map<SearchIndexData>(source);
        Assert.Multiple(() =>
        {
            Assert.That(actual.Score, Is.EqualTo(expected.Score));
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
        });
    }

    [Test()]
    public void Map_IndexDataToSearchIndexItemData()
    {
        var profile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(config => config.AddProfile(profile));
        var mapper = new Mapper(configuration);
        var source = new IndexData()
        {
            Score = 1.0,
            Id = "aa049c8d-ac12-45e2-a86d-ee90bfed5dd5",
            Title = "ほげほげ",
            Emoji = "🥺",
            Content = "ふがふが",
            Created = new DateTime(2020, 1, 1, 9, 0, 0, DateTimeKind.Utc),
            Updated = new DateTime(2022, 1, 1, 9, 0, 0, DateTimeKind.Utc),
            ETag = "9377e467-707f-4556-8829-1600a8442684"
        };
        var expected = new SearchIndexItemData()
        {
            Id = "aa049c8d-ac12-45e2-a86d-ee90bfed5dd5",
            Title = "ほげほげ",
            Emoji = "🥺",
            Content = "ふがふが",
            Created = new DateTime(2020, 1, 1, 9, 0, 0, DateTimeKind.Utc),
            Updated = new DateTime(2022, 1, 1, 9, 0, 0, DateTimeKind.Utc),
            ETag = "9377e467-707f-4556-8829-1600a8442684"
        };
        var actual = mapper.Map<SearchIndexItemData>(source);
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
            Assert.That(actual.Emoji, Is.EqualTo(expected.Emoji));
            Assert.That(actual.Content, Is.EqualTo(expected.Content));
            Assert.That(actual.Created, Is.EqualTo(expected.Created));
            Assert.That(actual.Updated, Is.EqualTo(expected.Updated));
            Assert.That(actual.ETag, Is.EqualTo(expected.ETag));
        });
    }

}
