// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ImageMdSyntaxNode : MdSyntaxNode<ImageMdSyntaxNode> {
    public string Href { get; set; } = string.Empty;
    public string AltText { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Href = string.Empty;
        AltText = string.Empty;
        Title = string.Empty;
        return base.TryReset();
    }
}
