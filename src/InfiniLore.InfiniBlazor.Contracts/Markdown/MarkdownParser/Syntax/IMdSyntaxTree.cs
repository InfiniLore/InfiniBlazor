// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxTree {
    IMdSyntaxNode RootNode { get; }

    IEnumerable<IMdSyntaxNode> VisitTopLevelNodes();
    IEnumerable<IMdSyntaxNode> VisitNodesBreadthFirst();
    IEnumerable<IMdSyntaxNode> VisitNodesDeepestFirst();
}
