// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class BlockQuoteMdSyntaxNode : MdSyntaxNode<BlockQuoteMdSyntaxNode> {
    public int LeadingSpaces { get; private set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public BlockQuoteMdSyntaxNode WithLeadingSpaces(int leadingSpaces) {
        LeadingSpaces = Math.Max(0, leadingSpaces);
        return this;
    }
    
    public override bool TryReset() {
        LeadingSpaces = 0;
        return base.TryReset();
    }

    protected override bool Equals(BlockQuoteMdSyntaxNode? other)
        => base.Equals(other)
            && LeadingSpaces == other.LeadingSpaces;
}
