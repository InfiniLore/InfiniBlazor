// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HeadingSimpleMdSyntaxNode : MdSyntaxNode<HeadingSimpleMdSyntaxNode> {
    public string Identifier { get; set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Identifier = string.Empty;
        return base.TryReset();
    }

    public override bool Equals(HeadingSimpleMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Identifier, other.Identifier);
}
