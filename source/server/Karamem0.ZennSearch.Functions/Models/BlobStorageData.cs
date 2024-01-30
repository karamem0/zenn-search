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

namespace Karamem0.ZennSearch.Models
{

    public class BlobStorageData
    {

        public string? Name { get; set; }

        public string? Title { get; set; }

        public string? Emoji { get; set; }

        public string? Content { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public string? ETag { get; set; }

    }

}
