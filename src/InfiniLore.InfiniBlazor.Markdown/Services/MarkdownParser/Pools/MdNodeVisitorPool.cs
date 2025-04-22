// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.MdNodes;
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.Pools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MdNodeVisitorPool {
    private const int InitialCapacity = 24;
    private static readonly ObjectPool<MdNodeVisitor> Pool =
        new DefaultObjectPool<MdNodeVisitor>(new DefaultPooledObjectPolicy<MdNodeVisitor>(), InitialCapacity);

    public static MdNodeVisitor Get() => Pool.Get();

    public static void Return(MdNodeVisitor visitor) {
        visitor.Node = null!;
        visitor.Depth = -1;
        Pool.Return(visitor);
    }
}
