// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed partial class ImageMdSyntaxNode : MdSyntaxNode<ImageMdSyntaxNode> {
    public string Href { get; private set; } = string.Empty;
    public string NormalizedAltText => NormalizeAltText.Replace(OriginalAltText, string.Empty);
    public string OriginalAltText { get; private set; } = string.Empty;

    [GeneratedRegex(@"\\(?!\\)")]
    private static partial Regex NormalizeAltText { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ImageMdSyntaxNode WithHref(string href) {
        Href = href;
        return this;
    }
    
    public ImageMdSyntaxNode WithAltText(string altText) {
        OriginalAltText = altText;
        return this;
    }   
    
    public override bool TryReset() {
        Href = string.Empty;
        OriginalAltText = string.Empty;

        return base.TryReset();
    }

    protected override bool Equals(ImageMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(OriginalAltText, other.OriginalAltText)
            && StringComparer.InvariantCulture.Equals(Href, other.Href);
}
