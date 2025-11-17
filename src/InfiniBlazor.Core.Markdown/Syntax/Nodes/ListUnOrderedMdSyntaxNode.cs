// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListUnOrderedMdSyntaxNode : MdSyntaxNode<ListUnOrderedMdSyntaxNode> {
    public int LeadingSpaces { get; private set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ListUnOrderedMdSyntaxNode WithLeadingSpaces(int leadingSpaces) {
        LeadingSpaces = Math.Max(0, leadingSpaces);
        return this;
    }
    
    public override bool TryReset() {
        LeadingSpaces = 0;
        return base.TryReset();
    }

    protected override bool Equals(ListUnOrderedMdSyntaxNode? other)
        => base.Equals(other)
            && LeadingSpaces == other.LeadingSpaces;
}
