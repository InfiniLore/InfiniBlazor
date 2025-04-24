// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownSyntaxTree {
    IMarkdownSyntaxNode RootNode { get; }

    IEnumerable<IMarkdownSyntaxVisitor> VisitNodesBreadthFirst();
    IEnumerable<IMarkdownSyntaxVisitor> VisitNodesDeepestFirst();
}
