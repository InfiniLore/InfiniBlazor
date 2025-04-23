// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Pools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class StringBuilderPool {
    private const int MaxRetained = 8;
    private static readonly ObjectPool<StringBuilder> Pool =
        new DefaultObjectPool<StringBuilder>(new DefaultPooledObjectPolicy<StringBuilder>(), MaxRetained);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static StringBuilder Get() => Pool.Get();
    public static void Return(StringBuilder builder) {
        builder.Clear();
        Pool.Return(builder);
    }
}
