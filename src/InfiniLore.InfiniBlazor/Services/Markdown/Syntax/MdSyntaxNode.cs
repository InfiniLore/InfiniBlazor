// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class MdSyntaxNode<T> : IMdSyntaxNode, IResettable
    where T : MdSyntaxNode<T>, new() 
{
    private const int ChildrenMinimumCapacity = 2;
    public int ChildCount { get; protected set; }
    protected virtual IMdSyntaxNode[] ChildNodes { get; set; } = GetInitialChildNodes(ChildrenMinimumCapacity);

    public virtual int Depth { get; set; }
    public IMdSyntaxNode? Parent { get; set; }
    public Type Type { get; } = typeof(T);
    
    [MemberNotNullWhen(true, nameof(Modifiers))] public bool ContainsModifiers => Modifiers is not null;
    public IMdSyntaxNodeModifier? Modifiers { get; set; }

    public static ObjectPool<T> Pool { get; } = PoolingHelpers.CreateResettablePool<T>(PoolingHelpers.VisitorPerParserRetained);
    // -----------------------------------------------------------------------------------------------------------------
    // Constructor Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected static IMdSyntaxNode[] GetInitialChildNodes(int count)
        => count != 0
            ? ArrayPool<IMdSyntaxNode>.Shared.Rent(count)
            : Array.Empty<IMdSyntaxNode>();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
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
    
    public TChild GetChildAt<TChild>(int index) where TChild : IMdSyntaxNode 
        => (TChild)ChildNodes[index];

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

    public void AddChildNode(IMdSyntaxNode childNode) {
        // Check if we need to resize
        EnsureChildNodeCapacity();
        childNode.WithParent(this);
        childNode.WithDepth(Depth + 1);

        // ReSharper disable once HeapView.PossibleBoxingAllocation
        ChildNodes[ChildCount++] = childNode;
    }

    public TChild AddChildNode<TChild>(TChild childNode) where TChild : IMdSyntaxNode {
        // Check if we need to resize
        EnsureChildNodeCapacity();
        childNode.WithParent(this);
        childNode.WithDepth(Depth + 1);

        // ReSharper disable once HeapView.PossibleBoxingAllocation
        ChildNodes[ChildCount++] = childNode;
        return childNode;
    }

    // ReSharper disable once InvertIf
    protected void EnsureChildNodeCapacity() {
        if (ChildCount == 0) {
            IMdSyntaxNode[] newArray = GetInitialChildNodes(ChildrenMinimumCapacity);
            ChildNodes = newArray;
        }

        if (ChildCount == ChildNodes.Length) {
            int newSize = Math.Max(ChildrenMinimumCapacity, Math.Max(1, ChildNodes.Length) * 2);
            IMdSyntaxNode[] newArray = ArrayPool<IMdSyntaxNode>.Shared.Rent(newSize);
            Array.Copy(ChildNodes, newArray, ChildCount);

            ArrayPool<IMdSyntaxNode>.Shared.Return(ChildNodes);
            ChildNodes = newArray;
        }
    }

    public IMdSyntaxNode WithContent(string content) {
        if (content.IsNullOrWhiteSpace()) return this;

        if (ChildNodes.LastOrDefault() is not ContentMdSyntaxNode lastNode) {
            ContentMdSyntaxNode newNode = ContentMdSyntaxNode.Pool.Get();
            newNode.Content = content;
            AddChildNode(newNode);
            return this;
        }

        int contentLength = lastNode.Content.Length;
        int length = contentLength + content.Length;
        lastNode.Content = string.Create(
            length,
            (contentLength, OriginalContent: lastNode.Content, NewContent: content),
            action: static (span, state) => {
                state.OriginalContent.AsSpan().CopyTo(span);
                state.NewContent.AsSpan().CopyTo(span[state.contentLength..]);
            });

        return this;
    }
    
    public IMdSyntaxNode WithDepth(int depth) {
        Depth = depth;
        return this;   
    }
    
    public IMdSyntaxNode WithParent(IMdSyntaxNode parent) {
        Parent = parent;
        return this;
    }

    public void ReturnToPool() {
        if (this is not T casted) throw new InvalidOperationException($"Cannot return {GetType()} to shared pool. Expected {typeof(T)}");

        TryReset();
        Pool.Return(casted);
    }

    public virtual bool TryReset() {
        if (ChildNodes.Length > 0) {
            ArrayPool<IMdSyntaxNode>.Shared.Return(ChildNodes, true);
            ChildNodes = ArrayPool<IMdSyntaxNode>.Shared.Rent(ChildrenMinimumCapacity);
        }

        ChildCount = 0;
        Depth = 0;
        Parent = null;
        
        // ReSharper disable once InvertIf
        if (Modifiers is not null) {
            MdSyntaxNodeModifier.Pool.Return(Unsafe.As<MdSyntaxNodeModifier>(Modifiers));
            Modifiers = null;
        }
        
        return true;
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Quick Generic Alternatives 
// ---------------------------------------------------------------------------------------------------------------------
public abstract class EmptyMdSyntaxNode<T> : MdSyntaxNode<T> where T : MdSyntaxNode<T>, new() {
    protected override IMdSyntaxNode[] ChildNodes { get; set; } = GetInitialChildNodes(0);// Will never have children so don't initialize
}
