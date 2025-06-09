// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using CodeOfChaos.Extensions.ObjectPool;
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IPoolCache>]
public class PoolCache : IPoolCache {
    public ObjectPool<StringBuilder> StringBuilderPool { get; } = new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy());
    public ObjectPool<Stack<Range>> RangeStackPool { get; } = CreateStackPool<Range>(16);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static ObjectPool<Stack<TItem>> CreateStackPool<TItem>(int maxRetained)  
        => new DefaultObjectPool<Stack<TItem>>(new StackPoolPolicy<Stack<TItem>,TItem>(), maxRetained);
}
