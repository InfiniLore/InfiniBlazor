// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.ObjectPool;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class PoolCache {
    public static readonly ObjectPool<RunningMarkdownParser> RunningMarkdownParserPool = CreateResettablePool<RunningMarkdownParser>(4);
    public static readonly ObjectPool<Dictionary<int, MdElement>> DepthCachePool = CreateCollectionPool<Dictionary<int, MdElement>, KeyValuePair<int, MdElement>>(2);
    public static readonly ObjectPool<MdNodeVisitor> MdNodeVisitorPool = CreateResettablePool<MdNodeVisitor>(24);
    public static readonly ObjectPool<ParserDataDto> ParserDataDtoPool = CreateResettablePool<ParserDataDto>(256);
    public static readonly ObjectPool<Stack<Range>> RangeStackPool = CreateStackPool<Stack<Range>, Range>(16);
    public static readonly ObjectPool<Stack<MdNodeVisitor>> MdNodeVisitorStackPool = CreateMdNodeVisitorStackPool(2);
    public static readonly ObjectPool<StringBuilder> StringBuilderPool = CreateStringBuilderPool(8);
    public static readonly ObjectPool<MdNode> MdNodePool = CreateResettablePool<MdNode>(256);
    public static readonly ObjectPool<Stack<MdNode>> MdNodeStackPool = CreateStackPool<Stack<MdNode>, MdNode>(16);
    public static readonly ObjectPool<MdNodeTree> MdNodeTreePool = CreateResettablePool<MdNodeTree>(8);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static ObjectPool<T> CreateResettablePool<T>(int maxRetained) where T : class, IResettable, new()
        => new DefaultObjectPool<T>(new ResettablePoolPolicy<T>(), maxRetained);

    private static ObjectPool<T> CreateCollectionPool<T, TItem>(int maxRetained) where T : class, ICollection<TItem>, new() 
        => new DefaultObjectPool<T>(new CollectionPoolPolicy<T, TItem>(), maxRetained);

    private static ObjectPool<T> CreateStackPool<T, TItem>(int maxRetained) where T : Stack<TItem>, new() 
        => new DefaultObjectPool<T>(new StackPoolPolicy<T,TItem>(), maxRetained);
    
    private static ObjectPool<Stack<MdNodeVisitor>> CreateMdNodeVisitorStackPool(int maxRetained) 
        => new DefaultObjectPool<Stack<MdNodeVisitor>>(new MdNodeVisitorStackPoolPolicy(MdNodeVisitorPool), maxRetained);
    
    private static ObjectPool<StringBuilder> CreateStringBuilderPool(int maxRetained)
        => new DefaultObjectPool<StringBuilder>(new StringBuilderPooledObjectPolicy {MaximumRetainedCapacity = int.MaxValue}, maxRetained);
}
