// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HeadingMdSyntaxNode : MdSyntaxNode<HeadingMdSyntaxNode> {
    public int Level { get; private set; } 
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public HeadingMdSyntaxNode WithLevel(int level) {
        Level = Math.Clamp(1, level, 6);
        return this;
    }
    
    public override bool TryReset() {
        Level = 0;
        return base.TryReset();
    }

    protected override bool Equals(HeadingMdSyntaxNode? other)
        => base.Equals(other)
            && Level == other.Level;
}
