// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.Pools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ParserDataDtoPool {
    private const int InitialCapacity = 255;
    private static readonly ObjectPool<ParserDataDto> Pool =
        new DefaultObjectPool<ParserDataDto>(new DefaultPooledObjectPolicy<ParserDataDto>(), InitialCapacity);

    public static ParserDataDto Get() => Pool.Get();

    public static void Return(ParserDataDto stack) {
        stack.Clear();
        Pool.Return(stack);
    }
}
