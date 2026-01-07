// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxTree : IEquatable<IMdSyntaxTree> {
    IRootMdSyntaxNode RootNode { get; }
    
    bool TryGetCachedChildrenByType<T>([NotNullWhen(true)] out IEnumerable<T>? nodes) where T : IMdSyntaxNode;
    bool TryGetCachedChildrenByType(Type type, [NotNullWhen(true)] out IEnumerable<IMdSyntaxNode>? nodes);
    
    IEnumerable<IMdSyntaxNode> VisitTopLevelNodes();
    IEnumerable<IMdSyntaxNode> VisitNodesBreadthFirst();
    IEnumerable<IMdSyntaxNode> VisitNodesDeepestFirst();

    void ClearCaches();

    int GetCount();
}
