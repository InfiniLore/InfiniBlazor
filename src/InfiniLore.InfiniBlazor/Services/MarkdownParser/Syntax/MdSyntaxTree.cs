// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MdSyntaxTree : IMdSyntaxTree, IResettable {
    public IMdSyntaxNode RootNode { get; private set; } = RootMdSyntaxNode.Pool.Get();
    
    public static ObjectPool<MdSyntaxTree> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxTree>(16);
    private static ObjectPool<Stack<IMdSyntaxNode>> MdSyntaxNodeStackPool { get; } = PoolingHelpers.CreateStackPool<IMdSyntaxNode>(PoolingHelpers.ParsersRetained);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Visit Nodes
    public IEnumerable<IMdSyntaxNode> VisitNodesBreadthFirst() {
        ReadOnlySpan<IMdSyntaxNode> rootNodeChildren = RootNode.GetChildrenSpan();
        int rootNodeChildCount = rootNodeChildren.Length;
        if (rootNodeChildCount == 0) yield break;// Early exit for empty tree nodes

        Stack<IMdSyntaxNode> stack = MdSyntaxNodeStackPool.Get();
        try {
            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = rootNodeChildCount - 1; i >= 0; i--) {
                stack.Push(rootNodeChildren[i]);
            }

            while (stack.TryPop(out IMdSyntaxNode? visitor)) {
                yield return visitor;

                // Push children in reverse order so they're processed in the correct order when popped
                ReadOnlySpan<IMdSyntaxNode> children = visitor.GetChildrenSpan();
                int childrenCount = children.Length;
                stack.EnsureCapacity(stack.Count + childrenCount);

                for (int i = childrenCount - 1; i >= 0; i--) {
                    stack.Push(children[i]);
                }
            }
        }
        finally {
            stack.Clear();
            MdSyntaxNodeStackPool.Return(stack);
        }
    }

    public IEnumerable<IMdSyntaxNode> VisitNodesDeepestFirst() {
        ReadOnlySpan<IMdSyntaxNode> rootNodeChildren = RootNode.GetChildrenSpan();
        int rootNodeChildCount = rootNodeChildren.Length;
        if (rootNodeChildCount == 0) yield break; // Early exit for empty trees

        Stack<IMdSyntaxNode> stackCache = MdSyntaxNodeStackPool.Get();
        Stack<IMdSyntaxNode> outputStack = MdSyntaxNodeStackPool.Get();

        try {
            stackCache.EnsureCapacity(rootNodeChildCount);
            outputStack.EnsureCapacity(rootNodeChildCount);

            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = 0; i < rootNodeChildCount; i++) {
                stackCache.Push(rootNodeChildren[i]);
            }

            // First pass: build the traversal order
            while (stackCache.TryPop(out IMdSyntaxNode? visitor)) {
                outputStack.Push(visitor);

                // Push children in normal order
                ReadOnlySpan<IMdSyntaxNode> children = visitor.GetChildrenSpan();
                int childrenCount = children.Length;
                stackCache.EnsureCapacity(stackCache.Count + childrenCount);
                outputStack.EnsureCapacity(outputStack.Count + childrenCount);

                for (int i = 0; i < childrenCount; i++) {
                    stackCache.Push(children[i]);
                }
            }

            // Second pass: yield the nodes in reverse order
            while (outputStack.TryPop(out IMdSyntaxNode? visitor)) {
                yield return visitor;
            }
        }
        finally {
            // Clean up both stacks
            MdSyntaxNodeStackPool.Return(stackCache);
            MdSyntaxNodeStackPool.Return(outputStack);
        }
    }
    #endregion

    public bool TryReset() {
        RootNode.Depth = 0;
        if (RootNode is not RootMdSyntaxNode rootNode) return false;

        // Using depth-first traversal with a single stack
        Stack<IMdSyntaxNode> stack = MdSyntaxNodeStackPool.Get();
        try {
            ReadOnlySpan<IMdSyntaxNode> children = rootNode.GetChildrenSpan();
            int childCount = children.Length;
            stack.EnsureCapacity(childCount);

            for (int i = 0; i < childCount; i++) {
                stack.Push(children[i]);
            }

            // Process all nodes depth-first
            while (stack.TryPop(out IMdSyntaxNode? node)) {
                children = node.GetChildrenSpan();
                childCount = children.Length;
                stack.EnsureCapacity(stack.Count + childCount);

                for (int i = 0; i < childCount; i++) {
                    stack.Push(children[i]);
                }
                node.ReturnToPool();
            }

            // Finally, reset and replace the root node
            RootNode.ReturnToPool();
            RootNode = RootMdSyntaxNode.Pool.Get();
            RootNode.Depth = 0;
            return true;
        }
        finally {
            MdSyntaxNodeStackPool.Return(stack);
        }
    }
}
