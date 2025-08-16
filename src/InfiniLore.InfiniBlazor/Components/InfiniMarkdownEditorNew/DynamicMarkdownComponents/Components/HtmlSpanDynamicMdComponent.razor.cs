// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Components.DynamicMarkdownComponents;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class HtmlSpanDynamicMdComponent {
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
