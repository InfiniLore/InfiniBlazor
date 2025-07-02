// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static partial class MarkdownRegexLib {
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
        | (?<linkNested>
            (?<lnBang>!)?
            \[(?<lnText>!?\[.+?\]\(.+?\))\]
            \((?<lnHref>http(?:s)?[^\)]+?)(?:\s?"(?<lnTitle>[^"]*)")?\)
          )
        | (?<linkRegular>
            (?<lrBang>!)?
            \[(?<lrText>[^\]]+?)\]
            \((?<lrHref>http(?:s)?[^\)]+?)(?:\s?"(?<lrTitle>[^"]*)")?\)
          )
        | (?<tag>\#(?<tText>[\p{L}\p{N}\-_/]+))
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex SinglelineStructuresRegex { get; }

    [GeneratedRegex("""
          (?<heading>^(?<hLevel>\#{1,6})[\ ]+(?<hText>[^\r\n]+)\r?\n?)
        | (?<codeBlock>`{3}(?<cLang>.*?)?\r?\n(?<cBody>[\s\S]*?)`{3})
        | (?<headingSimple>^(?<hsText>.+?)\r?\n[\ ]*[-=]{3,})
        | (?<listUnordered>(?:^[^\S\r\n]*-\s+.*(?:\r?\n(?:[^\S\r\n]*[\-.]\d*\.?\s+.*|[^\S\r\n]+.*))*))
        | (?<listOrdered>(?:^[^\S\r\n]*\d+\.\s+?.*(?:\r?\n(?:[^\S\r\n]*[\-.]?\d+\.?\s+.*|[^\S\r\n]+.*))*))
        | (?<table>
            ^\|(?<tHead>.+)\|\s*\r?\n
            ^\|(?<tSep>[:\-|\ ]+?)\|\s*\r?\n
            (?<tBody>(?:^\|.*\S.*\|(?:\r?\n|$))+)
          )
        | (?<blockQuote>^>\s+.*(?:\r?\n(?![*+-]\s|[-.]?\d|\s*[^>]).+)*)
        | (?:
            (?<htmlPre>.+?)?
              (?<htmlBody>
                <(?<tag>\w+)\b[^>]*>
                (?>
                  [^<]+
                  | <(?<open>\k<tag>)\b[^>]*>
                  | </(?<-open>\k<tag>)>
                  | <(?!/?\k<tag>\b)[^>]+>
                )*
                (?(open)(?!))
                (</\k<tag>>)
              )
            (?<htmlPost>.+)?
          )
        | (?<horizontalRule>^[\-=]{3,64}\s*$)
        | (?<paragraph>(?<p>.+?)(?:\r?\n|$))

        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex MultilineStructuresRegex { get; }

    public static ImmutableArray<string> MarkdownStructureGroupNames = [ 
        // Multiline
        "paragraph",
        "heading",
        "codeBlock",
        "headingSimple",
        "listUnordered",
        "listOrdered",
        "table",
        "blockQuote",
        "htmlBody",
        "horizontalRule",

        // Singleline
        "escaped",
        "bold",
        "italic",
        "supScript",
        "subScript",
        "strike",
        "code",
        "linkNested",
        "linkRegular",
        "underline",
        "emote",
        "tag"
    ];

    [GeneratedRegex(@"^[ ]*[-.]?\d*\.?\s+(?<lTask>\[[\ xX]\] )?(?<lHead>[^\r\n]+)(?<lBody>(?:\r?\n[ ]+.+)*)(?<!\r)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex ListItemBodyRegex { get; }

    [GeneratedRegex("\r?\n", RegexOptions.Compiled)]
    public static partial Regex NormalizeNewlinesRegex { get; }

    [GeneratedRegex("""
        (?<spanTag><(?<tag>span)\b[^>]*>)
        (?<spanBody>
          (?>
            [^<]+
            | <(?<open>\k<tag>)\b[^>]*>
            | </(?<-open>\k<tag>)>
            | <(?!/?\k<tag>\b)[^>]+>
          )*
        )
        (?(open)(?!))
        (</\k<tag>>)
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.Compiled)]
    public static partial Regex FindSpanHtmlRegex { get; }
    
    private static FrozenDictionary<string, int> GroupNameToGroupId { get; } = GetGroupNames();
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, int> GetGroupNames() {
        Regex[] regexes = [
            SinglelineStructuresRegex,
            MultilineStructuresRegex,
            FindSpanHtmlRegex,
            ListItemBodyRegex
        ];

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

    public static int GetSingleLineGroupId(string groupName) => GroupNameToGroupId[groupName];
    public static int GetMultiLineGroupId(string groupName) => GroupNameToGroupId[groupName];
    public static int GetSpanGroupId(string groupName) => GroupNameToGroupId[groupName];
    public static int GetListGroupId(string groupName) => GroupNameToGroupId[groupName];
    
}
