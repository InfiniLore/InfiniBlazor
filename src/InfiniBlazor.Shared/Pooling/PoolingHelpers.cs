// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.ObjectPool;
using Microsoft.Extensions.ObjectPool;

namespace InfiniBlazor.Pooling;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class PoolingHelpers {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static ObjectPool<T> CreateResettablePool<T>(int maxRetained) where T : class, IResettable, new()
        => new DefaultObjectPool<T>(new ResettablePoolPolicy<T>(), maxRetained);

    public static ObjectPool<Stack<TItem>> CreateStackPool<TItem>(int maxRetained)  
        => new DefaultObjectPool<Stack<TItem>>(new StackPoolPolicy<Stack<TItem>,TItem>(), maxRetained);
}
