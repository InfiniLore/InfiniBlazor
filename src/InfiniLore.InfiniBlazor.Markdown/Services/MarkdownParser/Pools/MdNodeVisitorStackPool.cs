// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.MdNodes;
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.Pools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MdNodeVisitorStackPool {
    private const int MaxRetained = 1;
    private static readonly ObjectPool<Stack<MdNodeVisitor>> Pool =
        new DefaultObjectPool<Stack<MdNodeVisitor>>(new DefaultPooledObjectPolicy<Stack<MdNodeVisitor>>(), MaxRetained);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static Stack<MdNodeVisitor> Get() => Pool.Get();
    public static void Return(Stack<MdNodeVisitor> stack) {
        while (stack.TryPop(out MdNodeVisitor? visitor)) {
            MdNodeVisitorPool.Return(visitor);
        }
        stack.Clear();
        Pool.Return(stack);
    }
}
