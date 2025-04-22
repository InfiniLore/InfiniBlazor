// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.Pools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class DepthCachePool {
    private const int InitialCapacity = 1;
    private static readonly ObjectPool< Dictionary<int, MdElement>> Pool =
        new DefaultObjectPool< Dictionary<int, MdElement>>(new DefaultPooledObjectPolicy< Dictionary<int, MdElement>>(), InitialCapacity);

    public static  Dictionary<int, MdElement> Get() => Pool.Get();

    public static void Return( Dictionary<int, MdElement> stack) {
        stack.Clear();
        Pool.Return(stack);
    }
}
