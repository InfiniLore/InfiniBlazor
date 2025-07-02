// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxTree : IMdSyntaxTree, IResettable {
    public IMdSyntaxNode RootNode { get; private set; } = RootMdSyntaxNode.Pool.Get();
    
    public static ObjectPool<MdSyntaxTree> Pool { get; } = Pooling.CreateResettablePool<MdSyntaxTree>(Pooling.ParsersRetained);
    private static ObjectPool<Stack<IMdSyntaxNode>> MdSyntaxNodeStackPool { get; } = Pooling.CreateStackPool<IMdSyntaxNode>(Pooling.ParsersRetained);
    private static ObjectPool<Stack<(int Depth, IMdSyntaxNode Node)>> DepthStackPool { get; } = Pooling.CreateStackPool<(int Depth, IMdSyntaxNode Node)>(4);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Visit Nodes
    public IEnumerable<(int Depth, IMdSyntaxNode Node)> VisitNodesBreadthFirst() {
        ReadOnlySpan<IMdSyntaxNode> rootNodeChildren = RootNode.GetChildrenSpan();
        int rootNodeChildCount = rootNodeChildren.Length;
        if (rootNodeChildCount == 0) yield break;// Early exit for empty tree nodes

        Stack<(int, IMdSyntaxNode)> stack = DepthStackPool.Get();
        try {
            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = rootNodeChildCount - 1; i >= 0; i--) {
                stack.Push((1, rootNodeChildren[i]));
            }

            while (stack.TryPop(out (int Depth, IMdSyntaxNode Node) visitor)) {
                int depth = visitor.Depth;
                IMdSyntaxNode currentNode = visitor.Node;

                yield return visitor;

                // Push children in reverse order so they're processed in the correct order when popped
                ReadOnlySpan<IMdSyntaxNode> children = currentNode.GetChildrenSpan();
                int childrenCount = children.Length;
                stack.EnsureCapacity(stack.Count + childrenCount);

                for (int i = childrenCount - 1; i >= 0; i--) {
                    stack.Push((depth + 1, children[i]));
                }
            }
        }
        finally {
            // Also handles MdNodeVisitorPool.Return(visitor) if the stack isn't empty yet
            DepthStackPool.Return(stack);
        }
    }

    public IEnumerable<(int Depth, IMdSyntaxNode Node)> VisitNodesDeepestFirst() {
        ReadOnlySpan<IMdSyntaxNode> rootNodeChildren = RootNode.GetChildrenSpan();
        int rootNodeChildCount = rootNodeChildren.Length;
        if (rootNodeChildCount == 0) yield break; // Early exit for empty trees

        Stack<(int, IMdSyntaxNode)> stackCache = DepthStackPool.Get();
        Stack<(int, IMdSyntaxNode)> outputStack = DepthStackPool.Get();

        try {
            stackCache.EnsureCapacity(rootNodeChildCount);
            outputStack.EnsureCapacity(rootNodeChildCount);

            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = 0; i < rootNodeChildCount; i++) {
                stackCache.Push((1, rootNodeChildren[i]));
            }

            // First pass: build the traversal order
            while (stackCache.TryPop(out (int Depth, IMdSyntaxNode Node) visitor)) {
                outputStack.Push(visitor);
                IMdSyntaxNode currentNode = visitor.Node;

                // Push children in normal order
                ReadOnlySpan<IMdSyntaxNode> children = currentNode.GetChildrenSpan();
                int childrenCount = children.Length;
                stackCache.EnsureCapacity(stackCache.Count + childrenCount);
                outputStack.EnsureCapacity(outputStack.Count + childrenCount);

                for (int i = 0; i < childrenCount; i++) {
                    stackCache.Push((visitor.Depth + 1, children[i]));
                }
            }

            // Second pass: yield the nodes in reverse order
            while (outputStack.TryPop(out (int Depth, IMdSyntaxNode Node) visitor)) {
                yield return visitor;
            }
        }
        finally {
            // Clean up both stacks
            DepthStackPool.Return(outputStack);
        }
    }
    #endregion

    public bool TryReset() {
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
            return true;
        }
        finally {
            MdSyntaxNodeStackPool.Return(stack);
        }
    }
}
