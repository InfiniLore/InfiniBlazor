// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EscapedCharacterMdSyntaxNode() : MdSyntaxNode<EscapedCharacterMdSyntaxNode>(initialChildCount:0) {
    public char Content { get; set; } = char.MinValue;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Content = char.MinValue;
        return base.TryReset();
    }
    
    public override bool Equals(EscapedCharacterMdSyntaxNode? other) => base.Equals(other)
        && Content == other.Content;
}
