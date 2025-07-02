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
public class MarkdownSyntaxTree : IMarkdownSyntaxTree, IResettable {
    public IMdSyntaxNode RootNode { get; private set; } = RootMdSyntaxNode.Shared.Get();

    public static MarkdownSyntaxTree WithRootNode(IMdSyntaxNode rootNode) {
        ArgumentNullException.ThrowIfNull(rootNode);
        return new MarkdownSyntaxTree { RootNode = rootNode };
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Visit Nodes
    public IEnumerable<IMarkdownSyntaxVisitor> VisitNodesBreadthFirst() {
        ReadOnlySpan<IMdSyntaxNode> rootNodeChildren = RootNode.GetChildrenSpan();
        int rootNodeChildCount = rootNodeChildren.Length;
        if (rootNodeChildCount == 0) yield break;// Early exit for empty tree nodes

        Stack<MarkdownSyntaxVisitor> stack = MarkdownPoolCache.MarkdownSyntaxVisitorStackPool.Get();
        try {
            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = rootNodeChildCount - 1; i >= 0; i--) {
                MarkdownSyntaxVisitor visitor = MarkdownPoolCache.MarkdownSyntaxVisitorPool.Get();
                visitor.Depth = 1;
                visitor.Node = rootNodeChildren[i];
                stack.Push(visitor);
            }

            while (stack.TryPop(out MarkdownSyntaxVisitor? visitor)) {
                int depth = visitor.Depth;
                IMdSyntaxNode currentNode = visitor.Node;

                yield return visitor;

                // Push children in reverse order so they're processed in the correct order when popped
                ReadOnlySpan<IMdSyntaxNode> children = currentNode.GetChildrenSpan();
                int childrenCount = children.Length;
                stack.EnsureCapacity(stack.Count + childrenCount);

                for (int i = childrenCount - 1; i >= 0; i--) {
                    MarkdownSyntaxVisitor childVisitor = MarkdownPoolCache.MarkdownSyntaxVisitorPool.Get();
                    childVisitor.Depth = depth + 1;
                    childVisitor.Node = children[i];
                    stack.Push(childVisitor);
                }

                MarkdownPoolCache.MarkdownSyntaxVisitorPool.Return(visitor);
            }
        }
        finally {
            // Also handles MdNodeVisitorPool.Return(visitor) if the stack isn't empty yet
            MarkdownPoolCache.MarkdownSyntaxVisitorStackPool.Return(stack);
        }
    }

    public IEnumerable<IMarkdownSyntaxVisitor> VisitNodesDeepestFirst() {
        ReadOnlySpan<IMdSyntaxNode> rootNodeChildren = RootNode.GetChildrenSpan();
        int rootNodeChildCount = rootNodeChildren.Length;
        if (rootNodeChildCount == 0) yield break; // Early exit for empty trees

        Stack<MarkdownSyntaxVisitor> stackCache = MarkdownPoolCache.MarkdownSyntaxVisitorStackPool.Get();
        Stack<MarkdownSyntaxVisitor> outputStack = MarkdownPoolCache.MarkdownSyntaxVisitorStackPool.Get();

        try {
            stackCache.EnsureCapacity(rootNodeChildCount);
            outputStack.EnsureCapacity(rootNodeChildCount);

            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = 0; i < rootNodeChildCount; i++) {
                MarkdownSyntaxVisitor visitor = MarkdownPoolCache.MarkdownSyntaxVisitorPool.Get();
                visitor.Depth = 1;
                visitor.Node = rootNodeChildren[i];
                stackCache.Push(visitor);
            }

            // First pass: build the traversal order
            while (stackCache.TryPop(out MarkdownSyntaxVisitor? visitor)) {
                outputStack.Push(visitor);
                IMdSyntaxNode currentNode = visitor.Node;

                // Push children in normal order
                ReadOnlySpan<IMdSyntaxNode> children = currentNode.GetChildrenSpan();
                int childrenCount = children.Length;
                stackCache.EnsureCapacity(stackCache.Count + childrenCount);
                outputStack.EnsureCapacity(outputStack.Count + childrenCount);

                for (int i = 0; i < childrenCount; i++) {
                    MarkdownSyntaxVisitor childVisitor = MarkdownPoolCache.MarkdownSyntaxVisitorPool.Get();
                    childVisitor.Depth = visitor.Depth + 1;
                    childVisitor.Node = children[i];
                    stackCache.Push(childVisitor);
                }
            }

            // Second pass: yield the nodes in reverse order
            while (outputStack.TryPop(out MarkdownSyntaxVisitor? visitor)) {
                yield return visitor;

                MarkdownPoolCache.MarkdownSyntaxVisitorPool.Return(visitor);
            }
        }
        finally {
            // Clean up both stacks
            MarkdownPoolCache.MarkdownSyntaxVisitorStackPool.Return(stackCache);
            MarkdownPoolCache.MarkdownSyntaxVisitorStackPool.Return(outputStack);
        }
    }
    #endregion

    public bool TryReset() {
        if (RootNode is not RootMdSyntaxNode rootNode) return false;

        // Using depth-first traversal with a single stack
        Stack<IMdSyntaxNode> stack = MarkdownPoolCache.MarkdownSyntaxNodeStackPool.Get();
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
                node.ReturnToShared();
            }

            // Finally, reset and replace the root node
            RootNode.ReturnToShared();
            RootNode = RootMdSyntaxNode.Shared.Get();
            return true;
        }
        finally {
            MarkdownPoolCache.MarkdownSyntaxNodeStackPool.Return(stack);
        }
    }
}
