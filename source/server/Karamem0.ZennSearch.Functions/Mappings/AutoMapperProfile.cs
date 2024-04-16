//
// Copyright (c) 2023-2024 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Models.Mappings;

public class AutoMapperProfile : Profile
{

    public static Assembly Assembly => typeof(AutoMapperProfile).Assembly;

    public AutoMapperProfile()
    {
        _ = this.CreateMap<IndexData, SearchIndexData>()
            .ForMember(d => d.Value, o => o.MapFrom(s => s));
        _ = this.CreateMap<IndexData, SearchIndexItemData>();
    }

}
