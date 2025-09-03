// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TagMdSyntaxNode() : MdSyntaxNode<TagMdSyntaxNode>(initialChildCount:0) {
    public string ContentTag { get; set; } = string.Empty;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        ContentTag = string.Empty;
        return base.TryReset();
    }
    
    public override bool Equals(TagMdSyntaxNode? other) => base.Equals(other)
        && ContentTag == other.ContentTag;
}
