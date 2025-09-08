// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.MdBlazorComponents;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class MdInfiniHtmlSpan {
    [GeneratedRegex("""style\s*=\s*["']([^"']*)["']""", RegexOptions.IgnoreCase)]
    private static partial Regex ExtractStyleAttributeRegex { get; }
    
    [GeneratedRegex("""class\s*=\s*["']([^"']*)["']""", RegexOptions.IgnoreCase)]
    private static partial Regex ExtractClassAttributeRegex { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static string? ExtractClassAttribute(string? htmlTag) {
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
    
    private static string? ExtractStyleAttribute(string? htmlTag) {
        if (htmlTag.IsNullOrEmpty()) return null;
        try {
            Match match = ExtractStyleAttributeRegex.Match(htmlTag);
            return match.Success
                ? match.Groups[1].Value
                : null;
        }
        catch (Exception) {
            return null;
        }
    }
}
