// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Text;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MdSyntaxTree : IMdSyntaxTree, IResettable {
    public IRootMdSyntaxNode RootNode {
        get;
        private set {
            if (ReferenceEquals(field, value)) return;

            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            field?.ReturnToPool();

            value.WithTreeReference(this);
            field = value;
        }
    } = null!;

    private static ObjectPool<Stack<IMdSyntaxNode>> MdSyntaxNodeStackPool { get; } = PoolingHelpers.CreateStackPool<IMdSyntaxNode>(16);

    public static IMdSyntaxTree Empty => new MdSyntaxTree();

    private ConcurrentDictionary<Type, IMdSyntaxNode[]> CachedChildrenByType { get; } = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public MdSyntaxTree() {
        InitializeRootNode(this);
    }

    private static void InitializeRootNode(MdSyntaxTree tree) {
        RootMdSyntaxNode rootNode = MdSyntaxNodePool<RootMdSyntaxNode>.Shared.Get();
        rootNode.WithTreeReference(tree);
        tree.RootNode = rootNode;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region CachedChildrenReferences
    public bool TryGetCachedChildrenByType<T>([NotNullWhen(true)] out IEnumerable<T>? nodes) where T : IMdSyntaxNode {
        if (!TryGetCachedChildrenByType(typeof(T), out IEnumerable<IMdSyntaxNode>? childNodes)) {
            nodes = null;
            return false;
        }

        nodes = childNodes.Cast<T>();
        return true;
    }

    public bool TryGetCachedChildrenByType(Type type, [NotNullWhen(true)] out IEnumerable<IMdSyntaxNode>? nodes) {
        nodes = null;
        if (!type.IsAssignableTo(typeof(IMdSyntaxNode))) return false;

        IMdSyntaxNode[] cached = CachedChildrenByType.GetOrAdd(
            type,
            static (t, tree) => tree.VisitNodesBreadthFirst()
                .Where(t.IsInstanceOfType)
                .ToArray(),
            this
        );

        if (cached.Length == 0) return false;

        nodes = cached;
        return true;
    }
    #endregion

    #region Visit Nodes
    // ReSharper disable once ConvertIfStatementToReturnStatement
    public IEnumerable<IMdSyntaxNode> VisitTopLevelNodes() {
        int childCount = RootNode.ChildCount;
        if (childCount == 0) return [];

        return RootNode.GetChildren();

    }

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
        if (rootNodeChildCount == 0) yield break;// Early exit for empty trees

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
    
    public void ClearCaches() {
        if (CachedChildrenByType.IsEmpty) return;
        CachedChildrenByType.Clear();
    }

    public int GetCount() {
        ReadOnlySpan<IMdSyntaxNode> rootNodeChildren = RootNode.GetChildrenSpan();
        int rootNodeChildCount = rootNodeChildren.Length;
        if (rootNodeChildCount == 0) return 0;// Early exit for empty tree nodes

        int count = 0;
        Stack<IMdSyntaxNode> stack = MdSyntaxNodeStackPool.Get();
        try {
            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = rootNodeChildCount - 1; i >= 0; i--) {
                stack.Push(rootNodeChildren[i]);
            }

            while (stack.TryPop(out IMdSyntaxNode? visitor)) {
                count++;

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

        return count;
    }
    #endregion

    public bool TryReset() {
        if (RootNode is not RootMdSyntaxNode rootNode) return false;// Cannot reset a non-root node

        CachedChildrenByType.Clear();

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
            InitializeRootNode(this);
            return true;
        }

        catch (Exception) {
            return false;
        }

        finally {
            MdSyntaxNodeStackPool.Return(stack);
        }
    }

    // ReSharper disable once NonReadonlyMemberInGetHashCode
    public override int GetHashCode()
        => HashCode.Combine(RootNode);

    public override bool Equals(object? obj)
        => obj is MdSyntaxTree casted
            && Equals(casted);

    public bool Equals(IMdSyntaxTree? other)
        => other is not null
            && RootNode.Equals(other.RootNode);

    public override string ToString() {
        StringBuilder sb = new();
        sb.AppendLine($"{GetType().Name}:");
        sb.AppendLine(RootNode.ToDebugString());

        ReadOnlySpan<IMdSyntaxNode> children = RootNode.GetChildrenSpan();
        for (int i = 0; i < children.Length; i++) {
            BuildTreeString(sb, children[i], "", i == children.Length - 1);
        }

        return sb.ToString();
    }

    private static void BuildTreeString(StringBuilder sb, IMdSyntaxNode node, string indent, bool isLast) {
        sb.Append(indent);
        sb.Append(isLast ? "└── " : "├── ");
        sb.AppendLine(node.ToDebugString());

        string nextIndent = indent + (isLast ? "    " : "│   ");

        ReadOnlySpan<IMdSyntaxNode> children = node.GetChildrenSpan();
        for (int i = 0; i < children.Length; i++) {
            BuildTreeString(sb, children[i], nextIndent, i == children.Length - 1);
        }
    }
}
