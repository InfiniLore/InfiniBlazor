// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using CodeOfChaos.Extensions.ObjectPool;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxDeserializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class BaseMdSyntaxDeserializer(IMdSyntaxNodeVisitor nodeVisitor) : IMdSyntaxDeserializer {
    private readonly Lock PoolLock = new();
    private static readonly ObjectPool<Dictionary<int, IMdSyntaxNode>> DepthCachePool = new DefaultObjectPool<Dictionary<int, IMdSyntaxNode>>(new CollectionPoolPolicy<Dictionary<int, IMdSyntaxNode>, KeyValuePair<int, IMdSyntaxNode>>(), 2);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkupString DeserializeToMarkupString(IMdSyntaxTree tree) 
        => new(DeserializeToString(tree));
    
    public string DeserializeToString(IMdSyntaxTree tree) {
        StringBuilder builder;
        Dictionary<int, IMdSyntaxNode> depthCache;
    
        // WTF why is a lock needed here? This makes no sense!
        //      Yes, it does, but poor Anna forgot why
        lock (PoolLock) {
            builder = GlobalPools.StringBuilder.Get();
            depthCache = DepthCachePool.Get();
        }
    
        try {
            int lastKnownDepth = -1;

            foreach (IMdSyntaxNode node in tree.VisitNodesBreadthFirst()) {
                int depth = node.Depth;
            
                lock (depthCache) {
                    if (lastKnownDepth + 1 > depth) 
                        CloseOpenTags(depthCache, depth, builder);

                    nodeVisitor.HandleOpenTag(node, builder);
                    nodeVisitor.HandleContent(node, builder);
                
                    depthCache.AddOrUpdate(depth, node);
                    lastKnownDepth = depth;
                }
            }

            CloseOpenTags(depthCache, -1, builder);
            return builder.ToString();
        }
        finally {
            builder.Clear();
            depthCache.Clear();
            lock (PoolLock) {
                GlobalPools.StringBuilder.Return(builder);
                DepthCachePool.Return(depthCache);
            }
        }
    }
    
    private void CloseOpenTags(Dictionary<int, IMdSyntaxNode> depthCache, int depth, StringBuilder builder) {
        if (depthCache.Count == 0) return;

        Span<int> keysToRemove = stackalloc int[depthCache.Count];
        int count = 0;

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (int k in depthCache.Keys) {
            if (k < depth) continue;
            keysToRemove[count++] = k;
        }

        Span<int> slice = keysToRemove[..count];
        slice.Sort();
        for (int i = count - 1; i >= 0; i--) {
            int key = slice[i];
            IMdSyntaxNode closingNode = depthCache[key];
            
            nodeVisitor.HandleCloseTag(closingNode, builder);
            depthCache.Remove(key);
        }
    }
}

[InjectableSingleton<IMdSyntaxDeserializer>]
public sealed class MdSyntaxDeserializer(IMdSyntaxNodeVisitor nodeVisitor) : BaseMdSyntaxDeserializer(nodeVisitor);

[InjectableSingleton<IMdSyntaxDeserializer>("styled")]
public sealed class StyledMdSyntaxDeserializer([FromKeyedServices("styled")] IMdSyntaxNodeVisitor nodeVisitor) : BaseMdSyntaxDeserializer(nodeVisitor);