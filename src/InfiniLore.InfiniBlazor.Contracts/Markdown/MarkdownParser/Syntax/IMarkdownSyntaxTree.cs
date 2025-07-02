// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownSyntaxTree {
    IMdSyntaxNode RootNode { get; }

    IEnumerable<IMarkdownSyntaxVisitor> VisitNodesBreadthFirst();
    IEnumerable<IMarkdownSyntaxVisitor> VisitNodesDeepestFirst();
}
