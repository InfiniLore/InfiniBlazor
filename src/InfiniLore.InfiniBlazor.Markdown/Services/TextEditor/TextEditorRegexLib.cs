// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static partial class TextEditorRegexLib {
    [GeneratedRegex(".*\r?\n", RegexOptions.Compiled | RegexOptions.ExplicitCapture)]
    public static partial Regex NewlinesRegex { get; }
    
    [GeneratedRegex("\r?\n", RegexOptions.Compiled | RegexOptions.ExplicitCapture)]
    public static partial Regex LineEndingRegex { get; }
    
    [GeneratedRegex(@"^( *(?:- |\d+\. |>[>\-\d. ]* )).*$", RegexOptions.Compiled)]
    public static partial Regex ListItemsRegex { get; }
    
    [GeneratedRegex(@"^(?!\s*\|?\s*-+\s*\|)(.*\|.*)$", RegexOptions.Compiled)]
    public static partial Regex IsTableLineRegex { get; }
    
    [GeneratedRegex(@"(?!\|)(\b[^\|\-\r\n]+\b)(?:(?=\|?))",RegexOptions.Compiled)]
    public static partial Regex TableCellsRegex { get; }
    
    [GeneratedRegex(@"\|[\|\-:\ ]+\|", RegexOptions.Compiled)]
    public static partial Regex IsTableHeaderLineRegex { get; }
}
