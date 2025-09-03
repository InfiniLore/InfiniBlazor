// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EscapedCharacterMdSyntaxNode() : MdSyntaxNode<EscapedCharacterMdSyntaxNode>(initialChildCount:0) {
    public char ContentChar { get; set; } = char.MinValue;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        ContentChar = char.MinValue;
        return base.TryReset();
    }
    
    public override bool Equals(EscapedCharacterMdSyntaxNode? other) => base.Equals(other)
        && ContentChar == other.ContentChar;
}
