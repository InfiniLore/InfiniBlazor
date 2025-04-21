// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.Pools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class RunningMarkdownParserPool {
    private static readonly ObjectPool<RunningMarkdownParser> Pool =
        ObjectPool.Create(new DefaultPooledObjectPolicy<RunningMarkdownParser>());

    public static RunningMarkdownParser Get() => Pool.Get();

    public static void Return(RunningMarkdownParser parser) {
        parser.Clear();
        Pool.Return(parser);
    }
}
