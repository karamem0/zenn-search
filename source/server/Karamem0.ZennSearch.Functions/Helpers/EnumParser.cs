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
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Helpers;

public static class EnumParser
{

    public static T Parse<T>(string? value, T defaultValue = default) where T : struct
    {
        return Enum.TryParse<T>(value, true, out var result) ? result : defaultValue;
    }

}
