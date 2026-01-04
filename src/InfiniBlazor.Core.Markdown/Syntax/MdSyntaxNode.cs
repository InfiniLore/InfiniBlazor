// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class MdSyntaxNode<T>(int initialChildCount = 2) : IMdSyntaxNode
    where T : MdSyntaxNode<T>, new() {
    public Guid Id { get; } = Guid.CreateVersion7();// Not reset during TryReset, this is by design.

    public int ChildCount { get; private set; }
    protected IMdSyntaxNode[] ChildNodes { get; private set; } = GetInitialChildNodeArray(initialChildCount);
    private readonly bool _isEmptyInitialized = GetEmptyInitializedState(initialChildCount);

    public int Depth { get; private set; }
    public IMdSyntaxNode? Parent { get; private set; }
    private static readonly Type TypeBacking = typeof(T);
    public Type Type => TypeBacking; 

    public IMdSyntaxNodeModifier? Modifier { get; private set; }
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static IMdSyntaxNode[] GetInitialChildNodeArray(int initialChildCount) => initialChildCount <= 0
        ? Array.Empty<IMdSyntaxNode>()
        : ArrayPool<IMdSyntaxNode>.Shared.Rent(Math.Max(1, initialChildCount));

    private static bool GetEmptyInitializedState(int initialChildCount)
        => initialChildCount <= 0;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region GetChild(ren)
    // ReSharper disable once ConvertIfStatementToReturnStatement
    public ReadOnlySpan<IMdSyntaxNode> GetChildrenSpan() {
        if (ChildCount == 0) return ReadOnlySpan<IMdSyntaxNode>.Empty;

        return ChildNodes.AsSpan(0, ChildCount);
    }

    public IEnumerable<IMdSyntaxNode> GetChildren() {
        for (int i = 0; i < ChildCount; i++) {
            yield return ChildNodes[i];
        }
    }

    public IEnumerable<TChild> GetChildrenByType<TChild>() where TChild : IMdSyntaxNode {
        for (int i = 0; i < ChildCount; i++) {
            IMdSyntaxNode child = ChildNodes[i];
            if (child is not TChild casted) continue;

            yield return casted;
        }
    }

    public IMdSyntaxNode GetChildAt(int index)
        => ChildNodes[index];

    public bool TryGetChildAt(int index, [NotNullWhen(true)] out IMdSyntaxNode? childNode) {
        if (index < 0 || index >= ChildCount) {
            childNode = null;
            return false;
        }

        childNode = ChildNodes[index];
        return true;
    }

    public bool TryGetChildAt<TChild>(int index, [NotNullWhen(true)] out TChild? childNode) where TChild : IMdSyntaxNode {
        if (index < 0 || index >= ChildCount || ChildNodes[index] is not TChild casted) {
            childNode = default;
            return false;
        }

        childNode = casted;
        return true;
    }
    #endregion

    #region GetNextSibling(s)
    public bool TryGetNextSibling([NotNullWhen(true)] out IMdSyntaxNode? mdSyntaxNode) {
        mdSyntaxNode = null;
        if (Parent is null) return false;

        ReadOnlySpan<IMdSyntaxNode> span = Parent.GetChildrenSpan();
        for (int i = 0; i < span.Length; i++) {
            if (!ReferenceEquals(span[i], this)) continue;

            if (i + 1 >= span.Length) return false;

            mdSyntaxNode = span[i + 1];
            return true;
        }

        // We should never get here because we are within our own parent
        return false;
    }

    public bool HasNextSibling() {
        if (Parent is null) return false;

        ReadOnlySpan<IMdSyntaxNode> span = Parent.GetChildrenSpan();
        for (int i = 0; i < span.Length; i++) {
            if (!ReferenceEquals(span[i], this)) continue;

            return i + 1 < span.Length;
        }

        // We should never get here because we are within our own parent
        return false;
    }
    #endregion
    
    #region GetPreviousSibling(s)
    public bool TryGetPreviousSibling([NotNullWhen(true)] out IMdSyntaxNode? mdSyntaxNode) {
        mdSyntaxNode = null;
        if (Parent is null) return false;

        ReadOnlySpan<IMdSyntaxNode> span = Parent.GetChildrenSpan();
        for (int i = span.Length - 1; i >= 0; i--) {
            if (!ReferenceEquals(span[i], this)) continue;

            if (i + 1 >= span.Length) return false;

            mdSyntaxNode = span[i + 1];
            return true;
        }

        // We should never get here because we are within our own parent
        return false;
    }

    public bool HasPreviousSibling() {
        if (Parent is null) return false;

        ReadOnlySpan<IMdSyntaxNode> span = Parent.GetChildrenSpan();
        for (int i = span.Length - 1; i >= 0; i--) {
            if (!ReferenceEquals(span[i], this)) continue;

            return i + 1 < span.Length;
        }

        // We should never get here because we are within our own parent
        return false;
    }
    #endregion
    
    #region Index
    public int GetIndexAtParent() {
        if (Parent is null) return -1;
        ReadOnlySpan<IMdSyntaxNode> span = Parent.GetChildrenSpan();
        for (int i = 0; i < span.Length; i++) {
            if (!ReferenceEquals(span[i], this)) continue;

            return i;
        }
        return -1;
    }
    #endregion
    
    #region AddChild(ren)
    public void AddChildNode(IMdSyntaxNode childNode) {
        // Check if we need to resize
        EnsureChildNodeExpansionCapacity();
        childNode.WithParent(this);
        childNode.WithDepth(Depth + 1);

        // ReSharper disable once HeapView.PossibleBoxingAllocation
        ChildNodes[ChildCount++] = childNode;
    }

    public TChild AddChildNode<TChild>(TChild childNode) where TChild : IMdSyntaxNode {
        // Check if we need to resize
        EnsureChildNodeExpansionCapacity();
        childNode.WithParent(this);
        childNode.WithDepth(Depth + 1);

        // ReSharper disable once HeapView.PossibleBoxingAllocation
        ChildNodes[ChildCount++] = childNode;
        return childNode;
    }

    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
    protected bool TryAddChildNodeAtIndex(int index, IMdSyntaxNode childNode) {
        if (index < 0 || index > ChildNodes.Length - 1) return false;
        if (ChildNodes[index] is not null) return false;

        EnsureChildNodeExpansionCapacity();
        childNode.WithParent(this);
        childNode.WithDepth(Depth + 1);

        // ReSharper disable once ConvertTypeCheckPatternToNullCheck
        if (ChildNodes[index] is IMdSyntaxNode existingNode) {
            existingNode.ReturnToPool();
            ChildCount--;
        }

        ChildNodes[index] = childNode;
        ChildCount++;
        return true;
    }

    // ReSharper disable once InvertIf
    private void EnsureChildNodeExpansionCapacity() {
        int childNodeArrayLength = ChildNodes.Length;
        if (childNodeArrayLength == 0 && _isEmptyInitialized) {
            // We are initializing with an empty array-shared object, so we need to initialize it from the pool, else we won't be able to return it to a pool
            ChildNodes = ArrayPool<IMdSyntaxNode>.Shared.Rent(2);
            return;
        }

        if (ChildCount + 1 <= childNodeArrayLength) return;

        int newSize = (childNodeArrayLength + 1) * 2;
        IMdSyntaxNode[] newArray = ArrayPool<IMdSyntaxNode>.Shared.Rent(newSize);
        Array.Copy(ChildNodes, newArray, ChildCount);

        ArrayPool<IMdSyntaxNode>.Shared.Return(ChildNodes);
        ChildNodes = newArray;
    }
    #endregion

    #region With...
    public IMdSyntaxNode WithText(string content) {
        if (ChildNodes.LastOrDefault() is not TextMdSyntaxNode lastNode) {
            TextMdSyntaxNode newNode = MdSyntaxNodePool<TextMdSyntaxNode>.Shared.Get();
            newNode.WithContent(content);
            AddChildNode(newNode);
            return this;
        }

        int contentLength = lastNode.Content.Length;
        int length = contentLength + content.Length;
        lastNode.WithContent(string.Create(
            length,
            (contentLength, OriginalContent: lastNode.Content, NewContent: content),
            action: static (span, state) => {
                state.OriginalContent.AsSpan().CopyTo(span);
                state.NewContent.AsSpan().CopyTo(span[state.contentLength..]);
            }));

        return this;
    }

    public IMdSyntaxNode WithParent(IMdSyntaxNode parent) {
        Parent = parent;
        return this;
    }
    
    public IMdSyntaxNode WithDepth(int depth) {
        Depth = depth;
        if (ChildCount == 0) return this;
        
        foreach (IMdSyntaxNode node in GetChildrenSpan()) {
            node.WithDepth(depth + 1);
        }
        
        return this;   
    }

    public IMdSyntaxNode WithModifier(IMdSyntaxNodeModifier modifier) {
        Modifier?.ReturnToPool();
        Modifier = modifier;
        return this;
    }

    public IMdSyntaxNode WithChild<TChild>(TChild child) where TChild : IMdSyntaxNode {
        AddChildNode(child);
        return this;
    }
    #endregion

    #region ReturnToPool and Cleanup
    void IMdSyntaxNode.ReturnToPool() => MdSyntaxNodePool<T>.Shared.Return(Unsafe.As<T>(this));

    public virtual bool TryReset() {
        if (ChildNodes.Length > 0) {
            ArrayPool<IMdSyntaxNode>.Shared.Return(ChildNodes, true);
            ChildNodes = GetInitialChildNodeArray(initialChildCount);
        }

        ChildCount = 0;
        Depth = 0;
        Parent = null;

        // ReSharper disable once InvertIf
        if (Modifier is not null) {
            MdSyntaxNodeModifier.Pool.Return(Unsafe.As<MdSyntaxNodeModifier>(Modifier));
            Modifier = null;
        }

        return true;
    }
    #endregion

    #region Equality
    // ReSharper disable once NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => HashCode.Combine(Id, ChildCount);

    public bool Equals(IMdSyntaxNode? other) => Equals(other as T);
    public override bool Equals(object? other) => Equals(other as T);

    protected virtual bool Equals([NotNullWhen(true)] T? other) {
        if (other is null) return false;
        if (ChildCount != other.ChildCount) return false;

        ReadOnlySpan<IMdSyntaxNode> span = other.GetChildrenSpan();
        for (int i = 0; i < ChildCount; i++) {
            if (!ChildNodes[i].Equals(span[i])) return false;
        }

        return Depth == other.Depth
            && (Parent is not null || other.Parent is null)
            && (Parent is null || other.Parent is not null)
            && Type == other.Type
            && (Modifier is null && other.Modifier is null
                || Modifier != null && Modifier.Equals(other.Modifier)
            );

    }
    #endregion

    #region ToString
    public virtual string ToDebugString() => GetType().Name;
    
    public override string ToString() => ToDebugString();
    #endregion
}
