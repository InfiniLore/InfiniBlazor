// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class RootMdSyntaxNode : MdSyntaxNode<RootMdSyntaxNode> {
    public override int Depth { get; set; } = 0; // root always starts at 0;

    public override bool TryReset() {
        if (!base.TryReset()) return false;
        Depth = 0;
        return true;
    }
}