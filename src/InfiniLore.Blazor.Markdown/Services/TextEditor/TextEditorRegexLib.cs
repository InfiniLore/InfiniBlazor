// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static partial class TextEditorRegexLib {
    [GeneratedRegex(".*\r?\n", RegexOptions.Compiled | RegexOptions.ExplicitCapture)]
    public static partial Regex NewlinesRegex { get; }
    
    [GeneratedRegex("\r?\n", RegexOptions.Compiled | RegexOptions.ExplicitCapture)]
    public static partial Regex LineEndingRegex { get; }
    
    [GeneratedRegex(@"^(?<a> *(?:- |\d+\. |>[>\-\d\. ]* ))(?<b>.*)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture)]
    public static partial Regex ListItemsRegex { get; }
}
