// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed partial class ImageMdSyntaxNode : MdSyntaxNode<ImageMdSyntaxNode> {
    public string Href { get; set; } = string.Empty;
    public string NormalizedAltText => NormalizeAltText.Replace(OriginalAltText, string.Empty);
    public string OriginalAltText { get; set; } = string.Empty;
    
    [GeneratedRegex(@"\\(?!\\)")]
    private static partial Regex NormalizeAltText { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Href = string.Empty;
        OriginalAltText = string.Empty;
        
        return base.TryReset();
    }
}
