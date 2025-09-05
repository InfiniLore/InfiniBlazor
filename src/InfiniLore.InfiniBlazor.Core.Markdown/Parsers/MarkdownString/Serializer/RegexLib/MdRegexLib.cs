// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.SpanLINQ;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static partial class MdRegexLib {
    [GeneratedRegex("""
          (?<escaped>\\\S)
        | (?<bold>\*\*(?<b>(?>[^\\\*]+|\\\*|\*|(?<open>\*\*)|(?<-open>\*\*))+?\*?)(?(open)(?!))\*\*)
        | (?<italic>\*(?<i>(?>[^\\\*]+|\\\*|\*\*|(?<open>\*)|(?<-open>\*))+)(?(open)(?!))\*)
        | (?<supScript>\^\^(?<sp>(?>[^\\\^]+|\\\^|\^|(?<open>\^\^)|(?<-open>\^\^))+?\^?)(?(open)(?!))\^\^)
        | (?<subScript>\^(?<sb>(?>[^\\\^]+|\\\^|\^\^|(?<open>\^)|(?<-open>\^))+)(?(open)(?!))\^)
        | (?<code>(?<open>`+)(?<c>(?>[^`\\]+|\\.|`(?!\k<open>))+?)\k<open>)
        | (?<strike>~(?<s>.+?)(?<!\\)~)
        | (?<underline>_(?<u>.+?)(?<!\\)_)
        | (?<emote>:(?<e>[\p{L}\p{N}\-_]+):)
        | (?<wikiLink>\[\[(?<wHref>[^\]\[\ ]+)\]\])
        | (?<template>(?<!\])(?<open>\{)+(?<t>[^\s{}]+)(?<-open>\})+(?(open)(?!)))
        | (?<link>
            (?<lnBang>!)?
            \[(?<lnText> (?:\ *!?\[.+?\]\(.+?\)\ *)|(?:[^\\\]]|\\\]|\\[^\]])*?)\]
            \(
              (?<lnHref>\ *https?[^\)\ |]+?)
              \ ?(?<lnMods>(?:\|.*)?)
            \)
          )
        | (?<tag>\#(?<tText>[\p{L}\p{N}\-_\/\.]+))
        | (?<user>\@(?<uName>[\p{L}\p{N}\-_\/\.]+))
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex SinglelineStructuresRegex { get; }

    [GeneratedRegex("""
          (?<heading>^(?<hLevel>\#{1,6})[\ ]+(?<hText>[^\n]+)$)
        | (?<codeBlock>`{3}(?<cLang>.*?)?\n(?<cBody>[\s\S]*?)`{3})
        | (?<headingSimple>^(?<hsText>.+?)\n(?<hsIdentifier>\ *(?:={3,}?|-{3,}?)\ *$))
        | (?<list>
            ^[^\S\n]*(?<lsId>-(?!-)|\d+\.|\.\d+).+
            (?:\n(?:(?:-(?!-)|\d+\.|\.\d+)|(?:\ +)).+)*
          )
        | (?<table>
            ^\|(?<tHead>.+)\|\ *\n
            ^\|(?<tSep>[:\-|\ ]+?)\|\ *
            (?<tBody>(?:\n(?:^\|.*\|$))+)
          )
        | (?<callout>
            ^>(?:\[!(?<clType>[^\|\n]+)(?<clMod>\|[^\n]*)?\](?<clOption>\+|\-)?)\ *(?<clTitle>[^\n]*)$
            (?:\n(?<clBody>>[^\n]*(?:\n>[^\n]*)*)$)?  
          )
        | (?<blockQuote>^>\ *(?<bqBody>.+(?:\n>[^\n]*)*)$)  
        | (?:
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
        | (?<horizontalRule>^(?<hr>\ *?(\-{3,}?|_{3,}?)\ *?)$)
        | (?<paragraph>(?<p>.+?)$)
        | (?<newLine>\n)

        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex MultilineStructuresRegex { get; }
    
    [GeneratedRegex(@"^ *(?:-|(?<lIndex>\d*)\.)(?:(?<lTaskSpace> *)\[(?<lTask>[ xX])])?(?:(?<lSpace> *)(?<lHead>.+)|(?<lHead> )|(?<lHead>))(?<lBody>(?:\n +.*)*)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
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
    
    private static FrozenDictionary<string, int> GroupNameToGroupId { get; } = GetGroupNames();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static Regex[] GetAllRegexes() => [
        SinglelineStructuresRegex,
        MultilineStructuresRegex,
        FindSpanHtmlRegex,
        ListItemBodyRegex,
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

    public static int GetGroupId(string groupName) {
        if (GroupNameToGroupId.TryGetValue(groupName, out int groupId)) return groupId;
        throw new ArgumentException($"No group name '{groupName}' found in any of the regexes.");
    }
}
