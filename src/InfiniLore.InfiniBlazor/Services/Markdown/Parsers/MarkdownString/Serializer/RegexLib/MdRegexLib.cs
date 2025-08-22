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
          (?<escaped>\\[][!"\#$%&'()*+,\-./:;<=>?@\\^_`{|}~])
        | (?<bold>\*\*(?<b>(?>[^\\\*]+|\\\*|\*|(?<open>\*\*)|(?<-open>\*\*))+?\*?)(?(open)(?!))\*\*)
        | (?<italic>\*(?<i>(?>[^\\\*]+|\\\*|\*\*|(?<open>\*)|(?<-open>\*))+)(?(open)(?!))\*)
        | (?<supScript>\^\^(?<sp>(?>[^\\\^]+|\\\^|\^|(?<open>\^\^)|(?<-open>\^\^))+?\^?)(?(open)(?!))\^\^)
        | (?<subScript>\^(?<sb>(?>[^\\\^]+|\\\^|\^\^|(?<open>\^)|(?<-open>\^))+)(?(open)(?!))\^)
        | (?<strike>~(?<s>.+?)(?<!\\)~)
        | (?<underline>_(?<u>.+?)(?<!\\)_)
        | (?<code>(?<open>`+)(?<c>(?>[^`\\]+|\\.|`(?!\k<open>))*?)\k<open>)
        | (?<emote>:(?<e>[\p{L}\p{N}\-_]+):)
        | (?<link>
            (?<lnBang>!)?
            \[(?<lnText> (?:\ *!?\[.+?\]\(.+?\)\ *)|(?:[^\\\]]|\\\]|\\[^\]])*?)\]
            \(
              (?<lnHref>\ *https?[^\)\ |]+?)
              \ ?(?<lnMods>(?:\|.*)?)
            \)
          )
        | (?<tag>\#(?<tText>[\p{L}\p{N}\-_/]+))
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex SinglelineStructuresRegex { get; }

    [GeneratedRegex("""
          (?<heading>^(?<hLevel>\#{1,6})[\ ]+(?<hText>[^\n]+)\n?)
        | (?<codeBlock>`{3}(?<cLang>.*?)?\n(?<cBody>[\s\S]*?)`{3})
        | (?<headingSimple>^(?<hsText>.+?)\n(?<hsIdentifier>[\ ]*[-=]{3,}))
        | (?<list>^[^\S\n]*(?<lsId>-|\d+\.)\s+.*(?:\n[^\S\n]+[^\n]+)*(?:\n[^\S\n]*(?:-|\d+\.)\s+.*(?:\n[^\S\n]+[^\n]+)*)*)
        | (?<table>
            ^\|(?<tHead>.+)\|\ *\n
            ^\|(?<tSep>[:\-|\ ]+?)\|\ *
            (?<tBody>(?:\n(?:^\|.*\|$))+)
          )
        | (?<callout>
            ^>\ *(?:\[!(?<clType>[^\|\n]+)(?<clMod>\|[^\n]*)?\](?<clOption>\+|\-)?)\ *(?<clTitle>[^\n]*)
            (?:\n(?<clBody>>[^\n]*(?:\n>[^\n]*)*))?  
          )
        | (?<blockQuote>^>\ *.+(?:\n>[^\n]*)*)  
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
        | (?<horizontalRule>^(?<hr>[\-=]{3,64})\s*$)
        | (?<paragraph>(?<p>.+?)(?:\n|$))
        | (?<emptyLine>\n)

        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex MultilineStructuresRegex { get; }

    public static readonly string[] MarkdownStructureGroupNames = [ 
        // Multiline
        MdRegexGroupNames.Paragraph,
        MdRegexGroupNames.Heading,
        MdRegexGroupNames.CodeBlock,
        MdRegexGroupNames.HeadingSimple,
        MdRegexGroupNames.List,
        MdRegexGroupNames.Table,
        MdRegexGroupNames.BlockQuote,
        MdRegexGroupNames.Callout,
        MdRegexGroupNames.HtmlBody,
        MdRegexGroupNames.HorizontalRule,
        MdRegexGroupNames.EmptyLine,

        // Singleline
        MdRegexGroupNames.Escaped,
        MdRegexGroupNames.BoldContent,
        MdRegexGroupNames.Italic,
        MdRegexGroupNames.SupScript,
        MdRegexGroupNames.SubScript,
        MdRegexGroupNames.Strike,
        MdRegexGroupNames.CodeInline,
        MdRegexGroupNames.Link,
        MdRegexGroupNames.Underline,
        MdRegexGroupNames.Emote,
        MdRegexGroupNames.Tag
    ];

    [GeneratedRegex(@"^ *(?:-|(?<lIndex>\d*)\.)\s+(?:\[(?<lTask>[ xX])] )?(?<lHead>[^\n]+)(?<lBody>(?:\n +.+)*)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex ListItemBodyRegex { get; }

    [GeneratedRegex("""
        (?<spanTag><(?<spanHtmlTag>span)(?<spanTagAttrs>\b[^>]*)>)
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
