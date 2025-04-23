// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.Pools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ParserDataDtoPool {
    private const int MaxRetained = 255;
    private static readonly ObjectPool<ParserDataDto> Pool =
        new DefaultObjectPool<ParserDataDto>(new DefaultPooledObjectPolicy<ParserDataDto>(), MaxRetained);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static ParserDataDto Get() => Pool.Get();
    public static void Return(ParserDataDto dto) {
        dto.Clear();
        Pool.Return(dto);
    }
}
