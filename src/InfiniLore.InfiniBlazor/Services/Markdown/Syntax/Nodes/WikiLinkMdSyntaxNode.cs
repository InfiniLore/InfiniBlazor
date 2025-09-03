// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class WikiLinkMdSyntaxNode() : MdSyntaxNode<WikiLinkMdSyntaxNode>(initialChildCount: 0) {
    public string Content { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public WikiLinkMdSyntaxNode WithContent(string content) {
        Content = content;
        return this;
    }
    
    public override bool TryReset() {
        Content = string.Empty;
        return base.TryReset();
    }

    public override bool Equals(WikiLinkMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Content, other.Content);
}
