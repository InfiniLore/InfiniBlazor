// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class LinkMdSyntaxNode : MdSyntaxNode<LinkMdSyntaxNode> {
    public string Href { get; set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Href = string.Empty;
        return base.TryReset();
    }

    public override bool Equals(LinkMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Href, other.Href);
}
