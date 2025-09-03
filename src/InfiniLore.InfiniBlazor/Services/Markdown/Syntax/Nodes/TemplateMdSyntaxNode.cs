// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TemplateMdSyntaxNode() : MdSyntaxNode<TemplateMdSyntaxNode>(initialChildCount: 0) {
    public string Content { get; set; } = string.Empty;
    public int BracesCount { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Content = string.Empty;
        BracesCount = 0;
        return base.TryReset();
    }

    public override bool Equals(TemplateMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Content, other.Content)
            && BracesCount == other.BracesCount;
}
