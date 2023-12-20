//
// Copyright (c) 2023 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/zenn-search/blob/main/LICENSE
//

using Azure.Storage.Blobs;
using Karamem0.ZennSearch.Helpers;
using Karamem0.ZennSearch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.ZennSearch.Services
{

    public class BlobStorageService
    {

        private readonly BlobContainerClient client;

        public BlobStorageService(BlobContainerClient client)
        {
            this.client = client;
        }

        public async IAsyncEnumerable<BlobStorageData> GetBlobsAsync()
        {
            await foreach (var blobPage in this.client.GetBlobsAsync().AsPages())
            {
                foreach (var name in blobPage.Values.Select(_ => _.Name))
                {
                    var blob = this.client.GetBlobClient(name);
                    var properties = await blob.GetPropertiesAsync();
                    var download = await blob.DownloadContentAsync();
                    var parser = new MarkdownParser(download.Value.Content.ToString());
                    yield return new BlobStorageData()
                    {
                        Name = Path.GetFileNameWithoutExtension(name),
                        Title = parser.Title,
                        Emoji = parser.Emoji,
                        Content = parser.Content,
                        Created = properties.Value.CreatedOn.UtcDateTime,
                        Updated = properties.Value.LastModified.UtcDateTime,
                        ETag = properties.Value.ETag.ToString()
                    };
                }
            }
        }

    }

}
