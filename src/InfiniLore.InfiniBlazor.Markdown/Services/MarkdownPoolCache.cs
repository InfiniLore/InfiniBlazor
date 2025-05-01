// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.ObjectPool;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MarkdownPoolCache {
    private const int ParsersRetained = 8;
    private const int NodesPerParserRetained = 96;
    private const int VisitorPerParserRetained = 16;
    
    public static readonly ObjectPool<MarkdownParserEngine> MarkdownParserEnginePool = CreateResettablePool<MarkdownParserEngine>(ParsersRetained);
    public static readonly ObjectPool<Dictionary<int, MarkdownElement>> DepthCachePool = CreateDictionaryPool<int, MarkdownElement>(ParsersRetained);
    public static readonly ObjectPool<Stack<MarkdownSyntaxVisitor>> MarkdownSyntaxVisitorStackPool = CreateMdNodeVisitorStackPool(ParsersRetained);
    public static readonly ObjectPool<Stack<MarkdownSyntaxNode>> MarkdownSyntaxNodeStackPool = CreateStackPool<MarkdownSyntaxNode>(ParsersRetained);
    public static readonly ObjectPool<MarkdownSyntaxTree> MarkdownSyntaxTreePool = CreateResettablePool<MarkdownSyntaxTree>(ParsersRetained);
    public static readonly ObjectPool<MarkdownSyntaxNode> MarkdownSyntaxNodePool = CreateResettablePool<MarkdownSyntaxNode>(ParsersRetained * NodesPerParserRetained);
    public static readonly ObjectPool<MarkdownFragment> MarkdownFragmentPool = CreateResettablePool<MarkdownFragment>(ParsersRetained * VisitorPerParserRetained);
    public static readonly ObjectPool<MarkdownSyntaxVisitor> MarkdownSyntaxVisitorPool = CreateResettablePool<MarkdownSyntaxVisitor>(ParsersRetained * VisitorPerParserRetained);
    
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
}
