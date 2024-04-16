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

public class UInt32ParserTests
{

    [Test()]
    public void Parse_Succeeded()
    {
        Assert.That(UInt32Parser.Parse("123", 0), Is.EqualTo(123));
    }

    [Test()]
    public void Parse_Failed()
    {
        Assert.That(UInt32Parser.Parse("abc", 123), Is.EqualTo(123));
    }

}
