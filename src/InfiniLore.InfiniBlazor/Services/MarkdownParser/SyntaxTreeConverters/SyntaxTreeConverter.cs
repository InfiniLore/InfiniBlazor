// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.ObjectPool;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.MarkdownParser.SyntaxTreeConverters;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SyntaxTreeConverter {
    private static readonly ObjectPool<Dictionary<int, IMdSyntaxNode>> DepthCachePool = new DefaultObjectPool<Dictionary<int, IMdSyntaxNode>>(new CollectionPoolPolicy<Dictionary<int, IMdSyntaxNode>, KeyValuePair<int, IMdSyntaxNode>>(), 2);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void ProcessNodeTree<TNodeConverter>(IMarkdownSyntaxTree tree, TNodeConverter nodeConverter)
        where TNodeConverter : ISyntaxNodeConverter  
    {
        Dictionary<int, IMdSyntaxNode> depthCache = DepthCachePool.Get();

        try {
            int lastKnownDepth = -1;

            foreach (IMarkdownSyntaxVisitor mdNodeVisitor in tree.VisitNodesBreadthFirst()) {
                int depth = mdNodeVisitor.Depth;
                IMdSyntaxNode node = mdNodeVisitor.Node;

                if (lastKnownDepth + 1 > depth) 
                    CloseOpenTags(nodeConverter, depthCache, depth);

                nodeConverter.HandleOpenTag(node);
                nodeConverter.HandleContent(node);
                
                depthCache.AddOrUpdate(depth, node);
                lastKnownDepth = depth;
            }

            CloseOpenTags(nodeConverter, depthCache, -1);
        }
        finally {
            DepthCachePool.Return(depthCache);
        }
    }
    
    private static void CloseOpenTags<TNodeConverter>(TNodeConverter nodeConverter, Dictionary<int, IMdSyntaxNode> depthCache, int depth)
        where TNodeConverter : ISyntaxNodeConverter  
    {
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
            
            nodeConverter.HandleCloseTag(closingNode);
            depthCache.Remove(key);
        }
    }
}
