// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TagMdSyntaxNode() : MdSyntaxNode<TagMdSyntaxNode>(initialChildCount:0) {
    public string Content { get; private set; } = string.Empty;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public TagMdSyntaxNode WithContent(string content) {
        Content = content;
        return this;
    }
    
    public override bool TryReset() {
        Content = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(TagMdSyntaxNode? other) => base.Equals(other)
        && StringComparer.InvariantCulture.Equals(Content, other.Content);
}
