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
public abstract class MdSyntaxNode<T> : IMdSyntaxNode, IResettable, IEquatable<T>
    where T : MdSyntaxNode<T>, new() 
{
    private readonly Guid _id = Guid.CreateVersion7();
    
    private const int ChildrenMinimumCapacity = 2;
    public int ChildCount { get; protected set; }
    protected virtual IMdSyntaxNode[] ChildNodes { get; set; } = GetInitialChildNodes(ChildrenMinimumCapacity);

    public virtual int Depth { get; private set; }
    public IMdSyntaxNode? Parent { get; private set; }
    public Type Type { get; } = typeof(T);
    
    private IMdSyntaxNodeModifier? Modifier { get; set; }

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
    
    public bool TryGetModifier([NotNullWhen(true)] out IMdSyntaxNodeModifier? mdSyntaxNodeModifier) {
        mdSyntaxNodeModifier = Modifier;
        return mdSyntaxNodeModifier is not null;
    }
    
    public bool TryGetNextSibling([NotNullWhen(true)] out IMdSyntaxNode? mdSyntaxNode) {
        mdSyntaxNode = null;
        if (Parent is null) return false;
        
        ReadOnlySpan<IMdSyntaxNode> span = Parent.GetChildrenSpan();
        for (int i = 0; i < span.Length; i++) {
            if (!Equals(span[i], this)) continue;
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
            if (!Equals(span[i], this)) continue;
            return i + 1 < span.Length;
        }
        
        // We should never get here because we are within our own parent
        return false;   
    }


    public void AddChildNode(IMdSyntaxNode childNode) {
        // Check if we need to resize
        EnsureChildNodeCapacity();
        childNode.WithParent(this);

        // ReSharper disable once HeapView.PossibleBoxingAllocation
        ChildNodes[ChildCount++] = childNode;
    }

    public TChild AddChildNode<TChild>(TChild childNode) where TChild : IMdSyntaxNode {
        // Check if we need to resize
        EnsureChildNodeCapacity();
        childNode.WithParent(this);

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
    
    public IMdSyntaxNode WithParent(IMdSyntaxNode parent) {
        Parent = parent;
        Depth = parent.Depth + 1;
        return this;
    }
    
    public IMdSyntaxNode WithModifier(IMdSyntaxNodeModifier modifier) {
        if (Modifier is MdSyntaxNodeModifier mod) {
            MdSyntaxNodeModifier.Pool.Return(mod);
        }
        Modifier = modifier;
        return this;   
    }
    
    public IMdSyntaxNode WithChild<TChild>(TChild child) where TChild : IMdSyntaxNode {
        AddChildNode(child);
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
        if (Modifier is not null) {
            MdSyntaxNodeModifier.Pool.Return(Unsafe.As<MdSyntaxNodeModifier>(Modifier));
            Modifier = null;
        }
        
        return true;
    }

    // ReSharper disable once NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => HashCode.Combine(_id, ChildCount);

    public bool Equals(IMdSyntaxNode? other) =>  Equals(other as T);
    public override bool Equals(object? other) => Equals(other as T);
    
    public virtual bool Equals(T? other) {
        if (other is null) return false;
        if (ChildCount != other.ChildCount) return false;

        ReadOnlySpan<IMdSyntaxNode> span = other.GetChildrenSpan();
        for (int i = 0; i < ChildCount; i++) {
            if (!ChildNodes[i].Equals(span[i])) return false; 
        }
        
        if (Depth != other.Depth) return false;
        
        if (Parent is null && other.Parent is not null) return false;
        if (Parent is not null && other.Parent is null) return false;
        
        if (Type != other.Type) return false;
        return Equals(Modifier, other.Modifier);
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Quick Generic Alternatives 
// ---------------------------------------------------------------------------------------------------------------------
public abstract class EmptyMdSyntaxNode<T> : MdSyntaxNode<T> where T : MdSyntaxNode<T>, new() {
    protected override IMdSyntaxNode[] ChildNodes { get; set; } = GetInitialChildNodes(0); // Will never have children so don't initialize
}
