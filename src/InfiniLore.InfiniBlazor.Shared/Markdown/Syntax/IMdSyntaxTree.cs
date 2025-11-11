// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxTree : IDisposable, IEquatable<IMdSyntaxTree> {
    IMdSyntaxNode RootNode { get; }
    
    void ReturnToPool();

    bool TryGetCachedChildrenByType<T>([NotNullWhen(true)] out IEnumerable<T>? nodes) where T : IMdSyntaxNode;
    bool TryGetCachedChildrenByType(Type type, [NotNullWhen(true)] out IEnumerable<IMdSyntaxNode>? nodes);
    void StoreChildAtCache<T>(T node) where T : IMdSyntaxNode;
    
    IEnumerable<IMdSyntaxNode> VisitTopLevelNodes();
    IEnumerable<IMdSyntaxNode> VisitNodesBreadthFirst();
    IEnumerable<IMdSyntaxNode> VisitNodesDeepestFirst();

    int GetCount();
}
