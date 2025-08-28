// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public partial class Infini_MdHtmlSpan {
    [GeneratedRegex("""class\s*=\s*["']([^"']*)["']""", RegexOptions.IgnoreCase)]
    private static partial Regex ExtractClassAttributeRegex { get; }
    
    private string? ExtractClassAttribute(string? htmlTag) {
        if (htmlTag.IsNullOrEmpty()) return null;
        try {
            Match match = ExtractClassAttributeRegex.Match(htmlTag);
            return match.Success
                ? match.Groups[1].Value
                : null;
        }
        catch (Exception) {
            return null;
        }
    }
}
