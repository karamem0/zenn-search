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

    public partial class MarkdownParser(string text)
    {

        [GeneratedRegex("[\\r\\n]+")]
        private static partial Regex RegexNewLine();

        [GeneratedRegex("^title:\\s*\"(.+?)\"\r?$", RegexOptions.Multiline)]
        private static partial Regex RegexTitle();

        [GeneratedRegex("^emoji:\\s*\"(.+?)\"\r?$", RegexOptions.Multiline)]
        private static partial Regex RegexEmoji();

        [GeneratedRegex("---(.+?)---\n", RegexOptions.Singleline)]
        private static partial Regex RegexFrontMatter();

        [GeneratedRegex(":::(.+?):::\n", RegexOptions.Singleline)]
        private static partial Regex RegexMessage();

        [GeneratedRegex("```(.+?)```\n", RegexOptions.Singleline)]
        private static partial Regex RegexPre();

        [GeneratedRegex("^#+\\s(.+?)$", RegexOptions.Multiline)]
        private static partial Regex RegexHeading();

        [GeneratedRegex("^\\s*[-\\*]\\s(.+?)$", RegexOptions.Multiline)]
        private static partial Regex RegexList();

        [GeneratedRegex("^\\s*[0-9]*\\.\\s(.+?)$", RegexOptions.Multiline)]
        private static partial Regex RegexNumber();

        [GeneratedRegex("^\\s*\\>\\s(.+?)$", RegexOptions.Multiline)]
        private static partial Regex RegexQuote();

        [GeneratedRegex("^@\\[(.+?)\\]\\((.+?)\\)$", RegexOptions.Multiline)]
        private static partial Regex RegexEmbedded();

        [GeneratedRegex("^https?://.+$", RegexOptions.Multiline)]
        private static partial Regex RegexUrl();

        [GeneratedRegex("\\*\\*(.+?)\\*\\*", RegexOptions.Multiline)]
        private static partial Regex RegexStrong();

        [GeneratedRegex("`(.+?)`", RegexOptions.Multiline)]
        private static partial Regex RegexCode();

        [GeneratedRegex("\\!?\\[(.*?)\\](?:\\((.+?)\\))?", RegexOptions.Multiline)]
        private static partial Regex RegexLink();

        private readonly string text = text;

        public string Title => RegexTitle().Match(this.text).Groups[1].Value;

        public string Emoji => RegexEmoji().Match(this.text).Groups[1].Value;

        public string Content
        {
            get
            {
                var value = this.text;
                value = RegexNewLine().Replace(value, "\n");
                value = RegexFrontMatter().Replace(value, "");
                value = RegexMessage().Replace(value, "");
                value = RegexPre().Replace(value, "");
                value = RegexHeading().Replace(value, "");
                value = RegexList().Replace(value, "$1");
                value = RegexNumber().Replace(value, "$1");
                value = RegexQuote().Replace(value, "$1");
                value = RegexEmbedded().Replace(value, "");
                value = RegexUrl().Replace(value, "");
                value = RegexStrong().Replace(value, "$1");
                value = RegexCode().Replace(value, "$1");
                value = RegexLink().Replace(value, "");
                return value.Replace("\n", "");
            }
        }

    }

}
