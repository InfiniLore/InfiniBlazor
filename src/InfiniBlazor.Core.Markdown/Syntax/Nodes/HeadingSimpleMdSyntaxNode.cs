// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HeadingSimpleMdSyntaxNode : MdSyntaxNode<HeadingSimpleMdSyntaxNode> {
    public string Identifier { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public HeadingSimpleMdSyntaxNode WithIdentifier(string identifier) {
        Identifier = identifier;
        return this;   
    }    
    
    public override bool TryReset() {
        Identifier = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(HeadingSimpleMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.Ordinal.Equals(Identifier, other.Identifier);
}
