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

    public static class GroupNames {
        public const string B = "b";
        public const string BlockQuote = "blockQuote";
        public const string Bold = "bold";
        public const string C = "c";
        public const string CBody = "cBody";
        public const string CLang = "cLang";
        public const string Code = "code"; 
        public const string CodeBlock = "codeBlock";
        public const string E = "e";
        public const string Emote = "emote";
        public const string Escaped = "escaped";
        public const string HLevel = "hLevel";
        public const string HText = "hText";
        public const string Heading = "heading";
        public const string HeadingSimple = "headingSimple";
        public const string HorizontalRule = "horizontalRule";
        public const string HsText = "hsText";
        public const string HtmlBody = "htmlBody";
        public const string HtmlPost = "htmlPost";
        public const string HtmlPre = "htmlPre";
        public const string I = "i";
        public const string Italic = "italic";
        public const string LBody = "lBody";
        public const string LHead = "lHead";
        public const string LTask = "lTask";
        public const string LinkNested = "linkNested";
        public const string LinkRegular = "linkRegular";
        public const string ListOrdered = "listOrdered";
        public const string ListUnordered = "listUnordered";
        public const string LnBang = "lnBang";
        public const string LnHref = "lnHref";
        public const string LnText = "lnText";
        public const string LnTitle = "lnTitle";
        public const string LrBang = "lrBang";
        public const string LrHref = "lrHref";
        public const string LrText = "lrText";
        public const string LrTitle = "lrTitle";
        public const string Open = "open";
        public const string P = "p";
        public const string Paragraph = "paragraph";
        public const string S = "s";
        public const string Sb = "sb";
        public const string Sp = "sp";
        public const string SpanBody = "spanBody";
        public const string SpanTag = "spanTag";
        public const string Strike = "strike";
        public const string SubScript = "subScript";
        public const string SupScript = "supScript";
        public const string TBody = "tBody";
        public const string THead = "tHead";
        public const string TSep = "tSep";
        public const string TText = "tText";
        public const string Table = "table";
        public const string Tag = "tag";
        public const string U = "u";
        public const string Underline = "underline"; 
    }
    
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
