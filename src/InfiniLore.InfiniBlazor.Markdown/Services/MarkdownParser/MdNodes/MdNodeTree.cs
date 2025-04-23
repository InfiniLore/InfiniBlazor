// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNodeTree : IMdNodeTree, IResettable {
    public IMdNode RootNode { get; private set; } = MdNode.AsRootNode();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Visit Nodes
    public IEnumerable<IMdNodeVisitor> VisitNodesBreadthFirst() {
        Stack<MdNodeVisitor> stack = PoolCache.MdNodeVisitorStackPool.Get();
        try {
            ReadOnlySpan<MdNode> rootNodeChildren = RootNode.GetChildrenSpan<MdNode>(out int rootNodeChildCount);
            if (rootNodeChildCount == 0) yield break;// Early exit for empty treMdNodees

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
        Stack<MdNodeVisitor> stackCache = PoolCache.MdNodeVisitorStackPool.Get();
        Stack<MdNodeVisitor> outputStack = PoolCache.MdNodeVisitorStackPool.Get();

        try {
            ReadOnlySpan<MdNode> rootNodeChildren = RootNode.GetChildrenSpan<MdNode>(out int rootNodeChildCount);
            if (rootNodeChildCount == 0) yield break;// Early exit for empty trees

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
        Stack<MdNode> cleanupStack = GetCleanupStack();
        while (cleanupStack.TryPop(out MdNode? node)) {
            PoolCache.MdNodePool.Return(node);
        }

        PoolCache.MdNodeStackPool.Return(cleanupStack);

        if (RootNode is not MdNode rootNode) return false;

        PoolCache.MdNodePool.Return(rootNode);
        RootNode = MdNode.AsRootNode();
        return true;
    }

    private Stack<MdNode> GetCleanupStack() {
        Stack<MdNode> stackCache = PoolCache.MdNodeStackPool.Get();
        Stack<MdNode> cleanupStack = PoolCache.MdNodeStackPool.Get();

        try {
            ReadOnlySpan<MdNode> rootNodeChildren = RootNode.GetChildrenSpan<MdNode>(out int rootNodeChildCount);

            stackCache.EnsureCapacity(rootNodeChildCount);
            cleanupStack.EnsureCapacity(rootNodeChildCount);

            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = 0; i < rootNodeChildCount; i++) {
                stackCache.Push(rootNodeChildren[i]);
            }

            // First pass: build the traversal order
            while (stackCache.TryPop(out MdNode? currentNode)) {
                cleanupStack.Push(currentNode);

                // Use the current node's children instead of root node's
                ReadOnlySpan<MdNode> children = currentNode.GetChildrenSpan<MdNode>(out int childrenCount);
                stackCache.EnsureCapacity(stackCache.Count + childrenCount);
                cleanupStack.EnsureCapacity(cleanupStack.Count + childrenCount);

                for (int i = 0; i < childrenCount; i++) {
                    stackCache.Push(children[i]);
                }
            }
        }
        catch {
            PoolCache.MdNodeStackPool.Return(cleanupStack);
            PoolCache.MdNodeStackPool.Return(stackCache);
            throw;
        }
        finally {
            PoolCache.MdNodeStackPool.Return(stackCache);
        }

        return cleanupStack;
    }

}
