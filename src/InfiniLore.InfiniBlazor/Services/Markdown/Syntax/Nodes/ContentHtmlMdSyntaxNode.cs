// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ContentHtmlMdSyntaxNode() : MdSyntaxNode<ContentHtmlMdSyntaxNode>(initialChildCount:0) {
    public string ContentHtml { get; set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        ContentHtml = string.Empty;
        return base.TryReset();
    }

    public override bool Equals(ContentHtmlMdSyntaxNode? other) => base.Equals(other)
        && ContentHtml == other.ContentHtml;
}
