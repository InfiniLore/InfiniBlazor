// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TextMdSyntaxNode() : MdSyntaxNode<TextMdSyntaxNode>(initialChildCount: 0) {
    public string Content { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public TextMdSyntaxNode WithContent(string content) {
        Content = content;
        return this;
    }
    
    public override bool TryReset() {
        Content = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(TextMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Content, other.Content);
}
