// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HorizontalRuleMdSyntaxNode() : MdSyntaxNode<HorizontalRuleMdSyntaxNode>(initialChildCount: 0) {
    public string Identifier { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public HorizontalRuleMdSyntaxNode WithIdentifier(string identifier) {
        Identifier = identifier;
        return this;   
    }    
    
    public override bool TryReset() {
        Identifier = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(HorizontalRuleMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Identifier, other.Identifier);
}
