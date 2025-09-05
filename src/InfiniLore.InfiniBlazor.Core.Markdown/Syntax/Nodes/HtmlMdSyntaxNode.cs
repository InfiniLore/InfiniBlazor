// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HtmlMdSyntaxNode() : MdSyntaxNode<HtmlMdSyntaxNode>(initialChildCount: 0) {
    public string Content { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public HtmlMdSyntaxNode WithContent(string content) {
        Content = content;
        return this;
    }
    
    public override bool TryReset() {
        Content = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(HtmlMdSyntaxNode? other)
        => base.Equals(other)
            && Content == other.Content;
}
