// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class BlockQuoteMdSyntaxNode : MdSyntaxNode<BlockQuoteMdSyntaxNode> {
    public int LeadingSpaces { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        LeadingSpaces = 0;
        return base.TryReset();
    }

    public override bool Equals(BlockQuoteMdSyntaxNode? other) => base.Equals(other) 
        && LeadingSpaces == other.LeadingSpaces ;
}
