// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownSyntaxTree {
    IMdSyntaxNode RootNode { get; }

    IEnumerable<(int Depth, IMdSyntaxNode Node)> VisitNodesBreadthFirst();
    IEnumerable<(int Depth, IMdSyntaxNode Node)> VisitNodesDeepestFirst();
}
