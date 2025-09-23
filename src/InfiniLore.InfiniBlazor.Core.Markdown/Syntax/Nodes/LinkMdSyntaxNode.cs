// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class LinkMdSyntaxNode : MdSyntaxNode<LinkMdSyntaxNode> {
    public string Href { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public LinkMdSyntaxNode WithHref(string href) {
        Href = href;
        return this;   
    }
    
    public LinkMdSyntaxNode WithTitle(string title) {
        Title = title;
        return this;
    }
    
    public override bool TryReset() {
        Href = string.Empty;
        Title = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(LinkMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.Ordinal.Equals(Href, other.Href)
            && StringComparer.Ordinal.Equals(Title, other.Title);
}
