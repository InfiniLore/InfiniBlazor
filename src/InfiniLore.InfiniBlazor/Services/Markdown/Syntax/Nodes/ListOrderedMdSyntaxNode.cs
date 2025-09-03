// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListOrderedMdSyntaxNode : MdSyntaxNode<ListOrderedMdSyntaxNode> {
    public int LeadingSpaces { get; private set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ListOrderedMdSyntaxNode WithLeadingSpaces(int leadingSpaces) {
        LeadingSpaces = Math.Max(0, leadingSpaces);
        return this;
    }
    
    public override bool TryReset() {
        LeadingSpaces = 0;
        return base.TryReset();
    }

    public override bool Equals(ListOrderedMdSyntaxNode? other)
        => base.Equals(other)
            && LeadingSpaces == other.LeadingSpaces;
}
