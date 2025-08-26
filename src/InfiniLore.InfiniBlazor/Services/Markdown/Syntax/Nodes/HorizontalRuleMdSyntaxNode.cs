// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HorizontalRuleMdSyntaxNode : EmptyMdSyntaxNode<HorizontalRuleMdSyntaxNode> {
    public string Identifier { get; set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Identifier = string.Empty;
        return base.TryReset();
    }
    
    public override bool Equals(HorizontalRuleMdSyntaxNode? other) => base.Equals(other)
        && Identifier == other.Identifier;
}
