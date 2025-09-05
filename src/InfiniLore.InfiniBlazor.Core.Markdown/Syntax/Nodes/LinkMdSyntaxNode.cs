// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class LinkMdSyntaxNode : MdSyntaxNode<LinkMdSyntaxNode> {
    public string Href { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public LinkMdSyntaxNode WithHref(string href) {
        Href = href;
        return this;   
    }
    
    public override bool TryReset() {
        Href = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(LinkMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Href, other.Href);
}
