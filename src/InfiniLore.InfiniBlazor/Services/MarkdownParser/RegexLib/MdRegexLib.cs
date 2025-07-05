// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
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
            \[(?<lnText>(?:!?\[.+?\]\(.+?\))|(?:[^\]]+?))\]
            \((?<lnHref>http(?:s)?[^\)]+?)(?:\s?"(?<lnTitle>[^"]*)")?\)
          )
        | (?<tag>\#(?<tText>[\p{L}\p{N}\-_/]+))
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex SinglelineStructuresRegex { get; }

    [GeneratedRegex("""
          (?<heading>^(?<hLevel>\#{1,6})[\ ]+(?<hText>[^\n]+)\n?)
        | (?<codeBlock>`{3}(?<cLang>.*?)?\n(?<cBody>[\s\S]*?)`{3})
        | (?<headingSimple>^(?<hsText>.+?)\n[\ ]*[-=]{3,})
        | (?<list>^[^\S\n]*(?<lsId>-|\d+\.)\s+.*(?:\n[^\S\n]+[^\n]+)*(?:\n[^\S\n]*(?:-|\d+\.)\s+.*(?:\n[^\S\n]+[^\n]+)*)*)
        | (?<table>
            ^\|(?<tHead>.+)\|\s*\n
            ^\|(?<tSep>[:\-|\ ]+?)\|\s*\n
            (?<tBody>(?:^\|.*\S.*\|(?:\n|$))+)
          )
        | (?<blockQuote>^>\s+.*(?:\n(?![*+-]\s|[-.]?\d|\s*[^>]).+)*)
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
        | (?<paragraph>(?<p>.+?)(?:\n|$))

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
        MdRegexGroupNames.HtmlBody,
        MdRegexGroupNames.HorizontalRule,

        // Singleline
        MdRegexGroupNames.Escaped,
        MdRegexGroupNames.Bold,
        MdRegexGroupNames.Italic,
        MdRegexGroupNames.SupScript,
        MdRegexGroupNames.SubScript,
        MdRegexGroupNames.Strike,
        MdRegexGroupNames.Code,
        MdRegexGroupNames.Link,
        MdRegexGroupNames.Underline,
        MdRegexGroupNames.Emote,
        MdRegexGroupNames.Tag
    ];

    [GeneratedRegex(@"^ *(?:-|\d*\.)\s+(?:\[(?<lTask>[ xX])] )?(?<lHead>[^\n]+)(?<lBody>(?:\n +.+)*)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    public static partial Regex ListItemBodyRegex { get; }

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
        ReadOnlySpan<Regex> regexes = [
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

    public static int GetGroupId(string groupName) {
        if (GroupNameToGroupId.TryGetValue(groupName, out int groupId)) return groupId;
        throw new ArgumentException($"No group name '{groupName}' found in any of the regexes.");
    }
}
