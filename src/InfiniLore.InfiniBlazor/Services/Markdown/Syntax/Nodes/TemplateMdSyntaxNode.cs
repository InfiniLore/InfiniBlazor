// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TemplateMdSyntaxNode() : MdSyntaxNode<TemplateMdSyntaxNode>(initialChildCount: 0) {
    public string Content { get; private set; } = string.Empty;
    public int BracesCount { get; private set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public TemplateMdSyntaxNode WithContent(string content) {
        Content = content;
        return this;
    }
    
    public TemplateMdSyntaxNode WithBracesCount(int bracesCount) {
        BracesCount = Math.Max(1, bracesCount);
        return this;
    }   
    
    public override bool TryReset() {
        Content = string.Empty;
        BracesCount = 0;
        return base.TryReset();
    }

    protected override bool Equals(TemplateMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Content, other.Content)
            && BracesCount == other.BracesCount;
}
