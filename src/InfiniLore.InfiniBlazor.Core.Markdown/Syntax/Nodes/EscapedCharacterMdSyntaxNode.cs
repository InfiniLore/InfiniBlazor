// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EscapedCharacterMdSyntaxNode() : MdSyntaxNode<EscapedCharacterMdSyntaxNode>(initialChildCount:0) {
    public char Content { get; private set; } = char.MinValue;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public EscapedCharacterMdSyntaxNode WithContent(char content) {
        Content = content;
        return this;
    }
    
    public override bool TryReset() {
        Content = char.MinValue;
        return base.TryReset();
    }

    protected override bool Equals(EscapedCharacterMdSyntaxNode? other)
        => base.Equals(other)
            && Content == other.Content;
}
