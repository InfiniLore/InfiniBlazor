// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxTree : IDisposable, IEquatable<IMdSyntaxTree> {
    IMdSyntaxNode RootNode { get; }

    IEnumerable<IMdSyntaxNode> VisitTopLevelNodes();
    IEnumerable<IMdSyntaxNode> VisitNodesBreadthFirst();
    IEnumerable<IMdSyntaxNode> VisitNodesDeepestFirst();
}
