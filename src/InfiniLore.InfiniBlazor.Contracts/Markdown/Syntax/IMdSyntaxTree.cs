// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxTree : IDisposable, IEquatable<IMdSyntaxTree> {
    IMdSyntaxNode RootNode { get; }
    
    void ReturnToPool();
    
    IEnumerable<IMdSyntaxNode> GetCachedChildrenByType<T>() where T : IMdSyntaxNode;
    IEnumerable<IMdSyntaxNode> GetCachedChildrenByType(Type type);
    void StoreChildAtCache<T>(T node) where T : IMdSyntaxNode;
    
    IEnumerable<IMdSyntaxNode> VisitTopLevelNodes();
    IEnumerable<IMdSyntaxNode> VisitNodesBreadthFirst();
    IEnumerable<IMdSyntaxNode> VisitNodesDeepestFirst();

    int GetCount();
}
