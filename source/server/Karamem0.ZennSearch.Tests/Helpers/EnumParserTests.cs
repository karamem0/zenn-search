//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Helpers.Tests;

public class EnumParserTests
{

    [Test()]
    public void Parse_Succeeded()
    {
        Assert.That(EnumParser.Parse("Local", DateTimeKind.Unspecified), Is.EqualTo(DateTimeKind.Local));
    }

    [Test()]
    public void Parse_Failed()
    {
        Assert.That(EnumParser.Parse(null, DateTimeKind.Unspecified), Is.EqualTo(DateTimeKind.Unspecified));
    }

}
