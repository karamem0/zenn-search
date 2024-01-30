//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Helpers
{

    public static partial class UInt32Parser
    {

        [GeneratedRegex("^[0-9]+$")]
        private static partial Regex RegexUInt32();

        public static uint Parse(string? value, uint defaultValue)
        {
            return RegexUInt32().IsMatch(value ?? "") ? uint.Parse(value!) : defaultValue;
        }

    }

}
