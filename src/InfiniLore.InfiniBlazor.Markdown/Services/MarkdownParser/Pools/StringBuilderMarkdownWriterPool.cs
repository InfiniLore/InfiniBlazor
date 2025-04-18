// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.MarkdownWriters;
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.Pools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class StringBuilderMarkdownWriterPool {
    private static readonly ObjectPool<StringBuilderMarkdownWriter> Pool =
        ObjectPool.Create(new DefaultPooledObjectPolicy<StringBuilderMarkdownWriter>());

    public static StringBuilderMarkdownWriter Get() => Pool.Get();

    public static void Return(StringBuilderMarkdownWriter builder) {
        builder.Clear();
        Pool.Return(builder);
    }
}
