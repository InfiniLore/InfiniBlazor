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
        if (RootNode.Children.Count == 0) yield break; // Early exit for empty trees

        Stack<MdNodeVisitor> stack = PoolCache.MdNodeVisitorStackPool.Get();
        try {
            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = RootNode.Children.Count - 1; i >= 0; i--) {
                if (RootNode.Children.ElementAtOrDefault(i) is not {} child) continue;

                MdNodeVisitor visitor = PoolCache.MdNodeVisitorPool.Get();
                visitor.Depth = 1;
                visitor.Node = child;
                stack.Push(visitor);
            }

            while (stack.TryPop(out MdNodeVisitor? visitor)) {
                int depth = visitor.Depth;
            
                yield return visitor;
                
                // Push children in reverse order so they're processed in the correct order when popped
                IReadOnlyList<IMdNode> children = visitor.Node.Children;
                int count = children.Count;
                stack.EnsureCapacity(stack.Count + count);

                for (int i = count - 1; i >= 0; i--) {
                    if (children.ElementAtOrDefault(i) is not {} child) continue;
                    
                    MdNodeVisitor childVisitor = PoolCache.MdNodeVisitorPool.Get();
                    childVisitor.Depth = depth + 1;
                    childVisitor.Node = child;
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
        if (RootNode.Children.Count == 0) yield break; // Early exit for empty trees
        
        Stack<MdNodeVisitor> stackCache = PoolCache.MdNodeVisitorStackPool.Get();
        Stack<MdNodeVisitor> outputStack = PoolCache.MdNodeVisitorStackPool.Get();
        int childCount = RootNode.Children.Count;
        stackCache.EnsureCapacity(childCount);
        outputStack.EnsureCapacity(childCount);
    
        try {
            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = 0; i < childCount; i++) {
                if (RootNode.Children.ElementAtOrDefault(i) is not {} child) continue;

                MdNodeVisitor visitor = PoolCache.MdNodeVisitorPool.Get();
                visitor.Depth = 1;
                visitor.Node = child;
                stackCache.Push(visitor);
            }

            // First pass: build the traversal order
            while (stackCache.TryPop(out MdNodeVisitor? visitor)) {
                outputStack.Push(visitor);
            
                // Push children in normal order
                IReadOnlyList<IMdNode> children = visitor.Node.Children;
                int count = children.Count;
                stackCache.EnsureCapacity(stackCache.Count + count);
                outputStack.EnsureCapacity(outputStack.Count + count);
                
                for (int i = 0; i < count; i++) {
                    if (children.ElementAtOrDefault(i) is not {} child) continue;
                
                    MdNodeVisitor childVisitor = PoolCache.MdNodeVisitorPool.Get();
                    childVisitor.Depth = visitor.Depth + 1;
                    childVisitor.Node = child;
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
        
        int childCount = RootNode.Children.Count;
        stackCache.EnsureCapacity(childCount);
        cleanupStack.EnsureCapacity(stackCache.Count);
    
        try {
            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = 0; i < childCount; i++) {
                if (RootNode.Children.ElementAtOrDefault(i) is not MdNode child) continue;
                stackCache.Push(child);
            }

            // First pass: build the traversal order
            while (stackCache.TryPop(out MdNode? visitor)) {
                cleanupStack.Push(visitor);
            
                // Push children in normal order
                IReadOnlyList<IMdNode> children = visitor.Children;
                int childrenCount = children.Count;
                stackCache.EnsureCapacity(stackCache.Count + childrenCount);
                cleanupStack.EnsureCapacity(cleanupStack.Count + childrenCount);
                
                for (int i = 0; i < children.Count; i++) {
                    if (children.ElementAtOrDefault(i) is not MdNode child) continue;
                    stackCache.Push(child);
                }
            }
        }
        catch {
            // Add error handling
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
