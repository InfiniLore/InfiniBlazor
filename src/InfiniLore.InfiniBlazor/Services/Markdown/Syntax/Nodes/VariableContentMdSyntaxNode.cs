// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class VariableContentMdSyntaxNode() : MdSyntaxNode<VariableContentMdSyntaxNode>(initialChildCount:0) {
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
    
    public override bool Equals(VariableContentMdSyntaxNode? other) => base.Equals(other)
        && Content == other.Content
        && BracesCount == other.BracesCount;
}
