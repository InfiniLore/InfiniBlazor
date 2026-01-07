// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class RootMdSyntaxNode : MdSyntaxNode<RootMdSyntaxNode>, IRootMdSyntaxNode {
    public IRootMdSyntaxNode WithTreeReference(IMdSyntaxTree mdSyntaxTree) {
        TreeReference = mdSyntaxTree;
        return this;
    }
}