// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNodeTree : IMdNodeTree, IResettable {
    public IMdNode RootNode { get; private set; } = PoolCache.MdNodePool.Get();

    public static MdNodeTree WithRootNode(IMdNode rootNode) {
        if (rootNode is not MdNode node) throw new ArgumentException("Root node must be of type MdNode.", nameof(rootNode));
        return new MdNodeTree { RootNode = node };
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Visit Nodes
    public IEnumerable<IMdNodeVisitor> VisitNodesBreadthFirst() {
        ReadOnlySpan<MdNode> rootNodeChildren = RootNode.GetChildrenSpan<MdNode>(out int rootNodeChildCount);
        if (rootNodeChildCount == 0) yield break;// Early exit for empty tree nodes

        Stack<MdNodeVisitor> stack = PoolCache.MdNodeVisitorStackPool.Get();
        try {
            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = rootNodeChildCount - 1; i >= 0; i--) {
                MdNodeVisitor visitor = PoolCache.MdNodeVisitorPool.Get();
                visitor.Depth = 1;
                visitor.Node = rootNodeChildren[i];
                stack.Push(visitor);
            }

            while (stack.TryPop(out MdNodeVisitor? visitor)) {
                int depth = visitor.Depth;
                IMdNode currentNode = visitor.Node;

                yield return visitor;

                // Push children in reverse order so they're processed in the correct order when popped
                ReadOnlySpan<MdNode> children = currentNode.GetChildrenSpan<MdNode>(out int childrenCount);
                stack.EnsureCapacity(stack.Count + childrenCount);

                for (int i = childrenCount - 1; i >= 0; i--) {
                    MdNodeVisitor childVisitor = PoolCache.MdNodeVisitorPool.Get();
                    childVisitor.Depth = depth + 1;
                    childVisitor.Node = children[i];
                    stack.Push(childVisitor);
                }

                PoolCache.MdNodeVisitorPool.Return(visitor);
            }
        }
        finally {
            // Also handles MdNodeVisitorPool.Return(visitor) if the stack isn't empty yet
            PoolCache.MdNodeVisitorStackPool.Return(stack);
        }
    }

    public IEnumerable<IMdNodeVisitor> VisitNodesDeepestFirst() {
        ReadOnlySpan<MdNode> rootNodeChildren = RootNode.GetChildrenSpan<MdNode>(out int rootNodeChildCount);
        if (rootNodeChildCount == 0) yield break; // Early exit for empty trees

        Stack<MdNodeVisitor> stackCache = PoolCache.MdNodeVisitorStackPool.Get();
        Stack<MdNodeVisitor> outputStack = PoolCache.MdNodeVisitorStackPool.Get();

        try {
            stackCache.EnsureCapacity(rootNodeChildCount);
            outputStack.EnsureCapacity(rootNodeChildCount);

            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = 0; i < rootNodeChildCount; i++) {
                MdNodeVisitor visitor = PoolCache.MdNodeVisitorPool.Get();
                visitor.Depth = 1;
                visitor.Node = rootNodeChildren[i];
                stackCache.Push(visitor);
            }

            // First pass: build the traversal order
            while (stackCache.TryPop(out MdNodeVisitor? visitor)) {
                outputStack.Push(visitor);
                IMdNode currentNode = visitor.Node;

                // Push children in normal order
                ReadOnlySpan<MdNode> children = currentNode.GetChildrenSpan<MdNode>(out int childrenCount);
                stackCache.EnsureCapacity(stackCache.Count + childrenCount);
                outputStack.EnsureCapacity(outputStack.Count + childrenCount);

                for (int i = 0; i < childrenCount; i++) {
                    MdNodeVisitor childVisitor = PoolCache.MdNodeVisitorPool.Get();
                    childVisitor.Depth = visitor.Depth + 1;
                    childVisitor.Node = children[i];
                    stackCache.Push(childVisitor);
                }
            }

            // Second pass: yield the nodes in reverse order
            while (outputStack.TryPop(out MdNodeVisitor? visitor)) {
                yield return visitor;

                PoolCache.MdNodeVisitorPool.Return(visitor);
            }
        }
        finally {
            // Clean up both stacks
            PoolCache.MdNodeVisitorStackPool.Return(stackCache);
            PoolCache.MdNodeVisitorStackPool.Return(outputStack);
        }
    }
    #endregion

    public bool TryReset() {
        if (RootNode is not MdNode rootNode) return false;

        // Using depth-first traversal with a single stack
        Stack<MdNode> stack = PoolCache.MdNodeStackPool.Get();
        try {
            ReadOnlySpan<MdNode> children = rootNode.GetChildrenSpan<MdNode>(out int childCount);
            stack.EnsureCapacity(childCount);

            for (int i = 0; i < childCount; i++) {
                stack.Push(children[i]);
            }

            // Process all nodes depth-first
            while (stack.TryPop(out MdNode? node)) {
                children = node.GetChildrenSpan<MdNode>(out childCount);
                stack.EnsureCapacity(stack.Count + childCount);

                for (int i = 0; i < childCount; i++) {
                    stack.Push(children[i]);
                }

                PoolCache.MdNodePool.Return(node);
            }

            // Finally, reset and replace the root node
            PoolCache.MdNodePool.Return(rootNode);
            RootNode = PoolCache.MdNodePool.Get();
            return true;
        }
        finally {
            PoolCache.MdNodeStackPool.Return(stack);
        }
    }
}
