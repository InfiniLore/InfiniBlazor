// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.Pools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class RunningMarkdownParserPool {
    private const int MaxRetained = 4;
    private static readonly ObjectPool<RunningMarkdownParser> Pool =
        new DefaultObjectPool<RunningMarkdownParser>(new DefaultPooledObjectPolicy<RunningMarkdownParser>(), MaxRetained);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static RunningMarkdownParser Get() => Pool.Get();
    public static void Return(RunningMarkdownParser parser) {
        parser.Clear();
        Pool.Return(parser);
    }
}
