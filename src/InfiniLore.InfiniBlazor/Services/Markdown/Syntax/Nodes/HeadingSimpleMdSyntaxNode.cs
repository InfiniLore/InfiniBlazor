// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HeadingSimpleMdSyntaxNode : MdSyntaxNode<HeadingSimpleMdSyntaxNode> {
    public string ContentIdentifier { get; set; } = string.Empty;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        ContentIdentifier = string.Empty;
        return base.TryReset();
    }
    
    public override bool Equals(HeadingSimpleMdSyntaxNode? other) => base.Equals(other)
        && ContentIdentifier == other.ContentIdentifier;
}
