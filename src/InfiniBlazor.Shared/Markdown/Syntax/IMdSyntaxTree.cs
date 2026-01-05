// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxTree : IEquatable<IMdSyntaxTree> {
    IMdSyntaxNode RootNode { get; }
    
    bool TryGetCachedChildrenByType<T>([NotNullWhen(true)] out IEnumerable<T>? nodes) where T : IMdSyntaxNode;
    bool TryGetCachedChildrenByType(Type type, [NotNullWhen(true)] out IEnumerable<IMdSyntaxNode>? nodes);
    void StoreChildAtCache<T>(T node) where T : IMdSyntaxNode;
    
    IEnumerable<IMdSyntaxNode> VisitTopLevelNodes();
    IEnumerable<IMdSyntaxNode> VisitNodesBreadthFirst();
    IEnumerable<IMdSyntaxNode> VisitNodesDeepestFirst();

    int GetCount();
}
