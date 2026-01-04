// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.SpanLINQ;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static partial class MdRegexLib {
    [GeneratedRegex(@"(?<escaped>\\\S)")]
    public static partial Regex EscapedCharacterRegex { get; }

    [GeneratedRegex(@"(?<bold>\*\*(?<b>(?>[^\\\*]+|\\\*|\*|(?<open>\*\*)|(?<-open>\*\*))+)(?(open)(?!))\*\*)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex BoldRegex { get; }

    [GeneratedRegex(@"(?<italic>\*(?<i>(?>[^\\\*]+|\\\*|\*\*|(?<open>\*)|(?<-open>\*))+)(?(open)(?!))\*)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex ItalicRegex { get; }

    [GeneratedRegex(@"(?<supScript>\^(?<sp>(?>[^\\\^\n]+|\\\^|\^\^|(?<open>\^)|(?<-open>\^))+)(?(open)(?!))\^)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex SuperScriptRegex { get; }

    [GeneratedRegex(@"(?<subScript>~(?<sb>(?>[^\\~\n]+|\\~|~~|(?<open>~)|(?<-open>~))+)(?(open)(?!))~)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex SubScriptRegex { get; }

    [GeneratedRegex(@"(?<code>(?<open>`+)(?<c>(?>[^`\\]+|\\.|`(?!\k<open>))+?)\k<open>)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex CodeInlineRegex { get; }

    [GeneratedRegex(@"(?<strike>~~(?<s>(?>[^\\~]+|\\~|~|(?<open>~~)|(?<-open>~~))+?~?)(?(open)(?!))~~)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex StrikeRegex { get; }

    [GeneratedRegex(@"(?<underline>_(?<u>(?>[^\\_]+|\\_|__|(?<open>_)|(?<-open>_))+)(?(open)(?!))_)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex UnderlineRegex { get; }

    [GeneratedRegex(@"(?<highlight>==(?<h>.+?)(?<!\\)==)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex HighlightRegex { get; }

    [GeneratedRegex(@"(?<emote>:(?<e>[\p{L}\p{N}_]+):)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex EmoteRegex { get; }

    [GeneratedRegex(@"(?<wikiLink>\[\[(?<wHref>[^\]\[\ ]+)\]\])", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex WikiLinkRegex { get; }

    [GeneratedRegex(@"(?<template>(?<!\])(?<open>\{)+(?<t>[^\s{}]+)(?<-open>\})+(?(open)(?!)))", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex TemplateRegex { get; }

    [GeneratedRegex("""
        (?<link>
            (?<lnBang>!)?
            \[(?<lnText> (?:\ *!?\[.+?\]\(.+?\)\ *)|(?:[^\\\]]|\\\]|\\[^\]])*?)\]
            \(
              (?<lnHref>\ *https?[^\ |]+)
              (?:\ ?\"(?<lnTitle>.+)\")?
              (?<lnMods>\|.*)?
            \)
        )
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex LinkRegex { get; }

    [GeneratedRegex(@"(?<tag>\#(?<tText>[\p{L}\p{N}\-_\/\.]+))", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex TagRegex { get; }

    [GeneratedRegex(@"(?<user>\@(?<uName>[\p{L}\p{N}\-_\/\.]+))", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex UserRegex { get; }

    [GeneratedRegex(@"(?<footnoteRef>\[\^(?<frId>[\d\p{L}\p{N}]+)\])", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex FootnoteReferenceRegex { get; }

    [GeneratedRegex(@"(?<wrapper><(?<wMods>\|.*?)>(?<w>.*)</>)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex WrapperRegex { get; }

    [GeneratedRegex(@"(?<break><[Bb][Rr]/?>)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex BreakRegex { get; }
    
    [GeneratedRegex(@"(?<heading>^(?<hLevel>\#{1,6})[\ ]+(?<hText>[^\n]+)$)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex HeadingRegex { get; }

    [GeneratedRegex(@"(?<codeBlock>^(?<open>`{3,})[\ ]*(?<cLang>.*?)?\n(?<cBody>(?>[\s\S]|(?!\k<open>))*?)\k<open>(?<cTrailing>[^\n]+)?$)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex CodeBlockRegex { get; }

    [GeneratedRegex(@"(?<headingSimple>^(?<hsText>.+?)\n(?<hsIdentifier>[\ ]*(?:={3,}?|-{3,}?)[\ ]*$))", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex HeadingSimpleRegex { get; }

    [GeneratedRegex("""
        (?<list>
            ^[^\S\n]*(?<lsId>-(?!-)|\d+\.|\.\d+).+
            (?:\n(?:(?:-(?!-)|\d+\.|\.\d+)|(?:[\ ]+)).+)*
        )
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex ListRegex { get; }

    [GeneratedRegex("""
        (?<table>
            ^\|(?<tHead>.+)\|[\ ]*\n
            ^\|(?<tSep>[:\-|\ ]+?)\|[\ ]*
            (?<tBody>(?:\n(?:^\|.*\|$))+)
        )
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex TableRegex { get; }

    [GeneratedRegex("""
        (?<callout>
            ^>(?:\[!(?<clType>[^\|\n]+)(?<clMod>\|[^\n]*)?\](?<clOption>\+|\-)?)[\ ]*(?<clTitle>[^\n]*)$
            (?:\n(?<clBody>>[^\n]*(?:\n>[^\n]*)*)$)?  
        )
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex CalloutRegex { get; }

    [GeneratedRegex(@"(?<blockQuote>^>[\ ]*(?:.+(?:\n>[^\n]*)*)$)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex BlockQuoteRegex { get; }

    [GeneratedRegex("""
        (?<footnoteDesc> 
            ^\[\^(?<fdId>[\d\p{L}\p{N}]+)\][\ ]?:[\ ]?(?<fdBody>.+
            (?:\n(?!\[)(?:.+))*)
        )
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex FootnoteDescriptionRegex { get; }

    [GeneratedRegex("""
        (?:
            (?<htmlPre>.+?)?
            (?<htmlBody>
                <(?<htmlTag>\w+)\b[^>]*>
                (?>
                    [^<]+
                    | <(?<open>\k<htmlTag>)\b[^>]*>
                    | </(?<-open>\k<htmlTag>)>
                    | <(?!/?\k<htmlTag>\b)[^>]+>
                )*
                (?(open)(?!))
                (</\k<htmlTag>>)
            )
            (?<htmlPost>.+)?
        )
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex HtmlBlockRegex { get; }

    [GeneratedRegex(@"(?<horizontalRule>^(?<hr>\ *?(\-{3,}?|_{3,}?)\ *?)$)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex HorizontalRuleRegex { get; }

    [GeneratedRegex("(?<paragraph>^(?<p>.+?)$)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex ParagraphRegex { get; }

    [GeneratedRegex(@"\n")]
    public static partial Regex NewLineRegex { get; }
    
    [GeneratedRegex(@"^ *(?:-|(?<lIndex>\d*)\.)(?:(?<lTaskSpace> *)\[(?<lTask>[ xX~])])?(?:(?<lSpace> *)(?<lHead>.+)|(?<lHead> )|(?<lHead>))(?<lBody>(?:\n +.*)*)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex ListItemBodyRegex { get; }

    [GeneratedRegex("""
        (?<spanTag><(?<spanHtmlTag>span)\ ?(?<spanTagAttrs>\b[^>]*)>)
        (?<spanBody>
          (?>
            [^<]+
            | <(?<open>\k<spanHtmlTag>)\b[^>]*>
            | </(?<-open>\k<spanHtmlTag>)>
            | <(?!/?\k<spanHtmlTag>\b)[^>]+>
          )*
        )
        (?(open)(?!))
        (</\k<spanHtmlTag>>)
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.Compiled)]
    public static partial Regex FindSpanHtmlRegex { get; }
    
    [GeneratedRegex(@"(?<frontmatter>(?<open>^-{3,}) *(?<fLang>.+)?\r?\n(?<fBody>[\s\S]*?)\r?\n\k<open>)", RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex FrontmatterRegex { get; }
    
    private static FrozenDictionary<string, int> GroupNameToGroupId { get; } = GetGroupNames();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static Regex[] GetAllRegexes() => [
        EscapedCharacterRegex,
        BoldRegex,
        ItalicRegex,
        SuperScriptRegex,
        SubScriptRegex,
        CodeInlineRegex,
        StrikeRegex,
        UnderlineRegex,
        HighlightRegex,
        EmoteRegex,
        WikiLinkRegex,
        TemplateRegex,
        LinkRegex,
        TagRegex,
        UserRegex,
        FootnoteReferenceRegex,
        WrapperRegex,
        BreakRegex,
        HeadingRegex,
        CodeBlockRegex,
        HeadingSimpleRegex,
        ListRegex,
        TableRegex,
        CalloutRegex,
        BlockQuoteRegex,
        FootnoteDescriptionRegex,
        HtmlBlockRegex,
        HorizontalRuleRegex,
        ParagraphRegex,
        NewLineRegex,
        FindSpanHtmlRegex,
        ListItemBodyRegex,
        FrontmatterRegex
    ];
    
    private static FrozenDictionary<string, int> GetGroupNames() {
        ReadOnlySpan<Regex> regexes = GetAllRegexes();

        int totalGroups = regexes.Sum(regex => regex.GetGroupNames().Length);
        Dictionary<string, int> dictionary = new(totalGroups);

        foreach (Regex regex in regexes) {
            string[] names = regex.GetGroupNames();
            foreach (string name in names) {
                dictionary.AddOrUpdate(name, regex.GroupNumberFromName(name));
            }
        }
        return dictionary.ToFrozenDictionary();
    }

    // ReSharper disable once ConvertIfStatementToReturnStatement
    public static int GetGroupId(string groupName) {
        if (GroupNameToGroupId.TryGetValue(groupName, out int groupId)) return groupId;
        throw new ArgumentException($"No group name '{groupName}' found in any of the regexes.");
    }
}