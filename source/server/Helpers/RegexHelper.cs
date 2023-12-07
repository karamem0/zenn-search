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

    public static partial class RegexHelper
    {

        [GeneratedRegex("^[0-9]+$")]
        private static partial Regex UInt32();

        [GeneratedRegex("[\\r\\n]+")]
        private static partial Regex NewLine();

        [GeneratedRegex("^title:\\s*\"(.+?)\"\\r?$", RegexOptions.Multiline)]
        private static partial Regex MarkdownTitle();

        [GeneratedRegex("^emoji:\\s*\"(.+?)\"\\r?$", RegexOptions.Multiline)]
        private static partial Regex MarkdownEmoji();

        [GeneratedRegex("---.+?---\n", RegexOptions.Singleline)]
        private static partial Regex MarkdownHeader();

        [GeneratedRegex(":::.+?:::\n", RegexOptions.Singleline)]
        private static partial Regex MarkdownMessage();

        [GeneratedRegex("```.+?```\n", RegexOptions.Singleline)]
        private static partial Regex MarkdownCodeBlock();

        public static uint ParseUInt32(string? value, uint defaultValue)
        {
            return UInt32().IsMatch(value ?? "") ? uint.Parse(value!) : defaultValue;
        }

        public static string GetMarkdownTitle(string text)
        {
            return MarkdownTitle().Match(text).Groups[1].Value;
        }

        public static string GetMarkdownEmoji(string text)
        {
            return MarkdownEmoji().Match(text).Groups[1].Value;
        }

        public static string GetMarkdownContent(string text)
        {
            text = NewLine().Replace(text, "\n");
            text = MarkdownHeader().Replace(text, "");
            text = MarkdownMessage().Replace(text, "");
            text = MarkdownCodeBlock().Replace(text, "");
            var lines = text.Split('\n', StringSplitOptions.None);
            var buffer = new StringBuilder();
            foreach (var line in lines.Select(_ => _.Trim()))
            {
                if (line.StartsWith("#") ||
                    line.StartsWith("-") ||
                    line.StartsWith("*") ||
                    line.StartsWith(">") ||
                    line.StartsWith("[") ||
                    line.StartsWith("![") ||
                    line.StartsWith("{{")
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
