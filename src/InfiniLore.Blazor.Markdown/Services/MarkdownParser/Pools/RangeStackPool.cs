// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.Blazor.Markdown.Services.Pools;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class RangeStackPool {
    private const int InitialCapacity = 16;
    private static readonly ObjectPool<Stack<Range>> Pool =
        new DefaultObjectPool<Stack<Range>>(new DefaultPooledObjectPolicy<Stack<Range>>(), InitialCapacity);

    public static Stack<Range> Get() => Pool.Get();

    public static void Return(Stack<Range> stack) {
        stack.Clear();
        Pool.Return(stack);
    }
}