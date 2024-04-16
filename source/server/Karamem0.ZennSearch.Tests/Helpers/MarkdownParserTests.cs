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

public class MarkdownParserTests
{

    [Test()]
    public void Title()
    {
        var text = "title: \"ほげほげ\"\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Title, Is.EqualTo("ほげほげ"));
    }

    [Test()]
    public void Emoji()
    {
        var text = "emoji: \"ほげほげ\"\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Emoji, Is.EqualTo("ほげほげ"));
    }

    [Test()]
    public void Content_NewLine()
    {
        var text = "ほげほげ\nふがふが\rぴよぴよ\r\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ほげほげふがふがぴよぴよ"));
    }

    [Test()]
    public void Content_FrontMatter()
    {
        var text = "---\ntitle: \"ほげほげ\"\nemoji: \"ふがふが\"\n---\nぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ぴよぴよ"));
    }

    [Test()]
    public void Content_Message()
    {
        var text = ":::message\nほげほげ\n:::\nふがふが\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ふがふが"));
    }

    [Test()]
    public void Content_Pre()
    {
        var text = "```\nほげほげ\n```\nふがふが\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ふがふが"));
    }

    [Test()]
    public void Content_Header()
    {
        var text = "# ほげほげ\n## ふがふが\nぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ぴよぴよ"));
    }

    [Test()]
    public void Content_List()
    {
        var text = "- ほげほげ\n* ふがふが\nぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ほげほげふがふがぴよぴよ"));
    }

    [Test()]
    public void Content_Number()
    {
        var text = "1. ほげほげ\n2. ふがふが\nぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ほげほげふがふがぴよぴよ"));
    }

    [Test()]
    public void Content_Quote()
    {
        var text = "> ほげほげ\n> ふがふが\nぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ほげほげふがふがぴよぴよ"));
    }

    [Test()]
    public void Content_Embedded()
    {
        var text = "@[ほげほげ](ふがふが)\nぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ぴよぴよ"));
    }

    [Test()]
    public void Content_Url()
    {
        var text = "http://ほげほげ\nhttps://ふがふが\nぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ぴよぴよ"));
    }

    [Test()]
    public void Content_Strong()
    {
        var text = "ほげほげ**ふがふが**ぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ほげほげふがふがぴよぴよ"));
    }

    [Test()]
    public void Content_Code()
    {
        var text = "ほげほげ`ふがふが`ぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ほげほげふがふがぴよぴよ"));
    }

    [Test()]
    public void Content_Link()
    {
        var text = "![](ほげほげ)[ふがふが]ぴよぴよ\n";
        var parser = new MarkdownParser(text);
        Assert.That(parser.Content, Is.EqualTo("ぴよぴよ"));
    }

}
