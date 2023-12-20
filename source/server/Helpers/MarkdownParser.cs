//
// Copyright (c) 2023 karamem0
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

    public partial class MarkdownParser
    {

        [GeneratedRegex("[\\r\\n]+")]
        private static partial Regex RegexNewLine();

        [GeneratedRegex("^title:\\s*\"(.+?)\"\\r?$", RegexOptions.Multiline)]
        private static partial Regex RegexTitle();

        [GeneratedRegex("^emoji:\\s*\"(.+?)\"\\r?$", RegexOptions.Multiline)]
        private static partial Regex RegexEmoji();

        [GeneratedRegex("---(.+?)---\n", RegexOptions.Singleline)]
        private static partial Regex RegexHeader();

        [GeneratedRegex(":::(.+?):::\n", RegexOptions.Singleline)]
        private static partial Regex RegexMessage();

        [GeneratedRegex("```(.+?)```\n", RegexOptions.Singleline)]
        private static partial Regex RegexPre();

        [GeneratedRegex("\\*\\*(.+?)\\*\\*", RegexOptions.Multiline)]
        private static partial Regex RegexStrong();

        [GeneratedRegex("`(.+?)`", RegexOptions.Multiline)]
        private static partial Regex RegexCode();

        private readonly string text;

        public MarkdownParser(string text)
        {
            this.text = text;
        }

        public string Title => RegexTitle().Match(this.text).Groups[1].Value;

        public string Emoji => RegexEmoji().Match(this.text).Groups[1].Value;

        public string Content
        {
            get
            {
                var value = this.text;
                value = RegexNewLine().Replace(value, "\n");
                value = RegexHeader().Replace(value, "");
                value = RegexMessage().Replace(value, "");
                value = RegexPre().Replace(value, "");
                value = RegexStrong().Replace(value, "$1");
                value = RegexCode().Replace(value, "$1");
                var lines = value.Split('\n', StringSplitOptions.None);
                var buffer = new StringBuilder();
                foreach (var line in lines.Select(_ => _.Trim()))
                {
                    if (line.StartsWith("#") ||
                        line.StartsWith("-") ||
                        line.StartsWith("*") ||
                        line.StartsWith(">") ||
                        line.StartsWith("[") ||
                        line.StartsWith("![") ||
                        line.StartsWith("{{") ||
                        line.StartsWith("http")
                    )
                    {
                        continue;
                    }
                    _ = buffer.Append(line);
                }
                return buffer.ToString();
            }
        }

    }

}
