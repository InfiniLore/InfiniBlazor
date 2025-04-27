// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.ObjectPool;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class PoolCache {
    public static readonly ObjectPool<MarkdownParserEngine> RunningMarkdownParserPool = CreateResettablePool<MarkdownParserEngine>(4);
    public static readonly ObjectPool<Dictionary<int, MarkdownElement>> DepthCachePool = CreateDictionaryPool<int, MarkdownElement>(2);
    public static readonly ObjectPool<MarkdownSyntaxVisitor> MarkdownSyntaxVisitorPool = CreateResettablePool<MarkdownSyntaxVisitor>(24);
    public static readonly ObjectPool<MarkdownFragment> MarkdownFragmentPool = CreateResettablePool<MarkdownFragment>(256);
    public static readonly ObjectPool<Stack<Range>> RangeStackPool = CreateStackPool<Range>(16);
    public static readonly ObjectPool<Stack<MarkdownSyntaxVisitor>> MMarkdownSyntaxVisitorStackPool = CreateMdNodeVisitorStackPool(4);
    public static readonly ObjectPool<StringBuilder> StringBuilderPool = CreateStringBuilderPool(8);
    public static readonly ObjectPool<MarkdownSyntaxNode> MdNodePool = CreateResettablePool<MarkdownSyntaxNode>(256);
    public static readonly ObjectPool<Stack<MarkdownSyntaxNode>> MdNodeStackPool = CreateStackPool<MarkdownSyntaxNode>(16);
    public static readonly ObjectPool<MarkdownSyntaxTree> MdNodeTreePool = CreateResettablePool<MarkdownSyntaxTree>(8);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static ObjectPool<T> CreateResettablePool<T>(int maxRetained) where T : class, IResettable, new()
        => new DefaultObjectPool<T>(new ResettablePoolPolicy<T>(), maxRetained);

    private static ObjectPool<Dictionary<TKey, TValue>> CreateDictionaryPool<TKey, TValue>(int maxRetained) where TKey : notnull 
        => new DefaultObjectPool<Dictionary<TKey, TValue>>(new CollectionPoolPolicy<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>(), maxRetained);

    private static ObjectPool<Stack<TItem>> CreateStackPool<TItem>(int maxRetained)  
        => new DefaultObjectPool<Stack<TItem>>(new StackPoolPolicy<Stack<TItem>,TItem>(), maxRetained);
    
    private static ObjectPool<Stack<MarkdownSyntaxVisitor>> CreateMdNodeVisitorStackPool(int maxRetained) 
        => new DefaultObjectPool<Stack<MarkdownSyntaxVisitor>>(new MarkdownSyntaxVisitorStackPoolPolicy(MarkdownSyntaxVisitorPool), maxRetained);
    
    private static ObjectPool<StringBuilder> CreateStringBuilderPool(int maxRetained)
        => new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy {MaximumRetainedCapacity = int.MaxValue}, maxRetained);
}
