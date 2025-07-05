// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Buffers;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class MdSyntaxNode<T> : IMdSyntaxNode, IResettable
    where T : MdSyntaxNode<T>, new() 
{
    public static ObjectPool<T> Pool { get; } = PoolingHelpers.CreateResettablePool<T>(16);

    private const int ChildrenMinimumCapacity = 2;
    public int ChildCount { get; private set; }
    public virtual int Depth { get; set; }
    
    protected virtual IMdSyntaxNode[] ChildNodes { get; set; } = GetInitialChildNodes(ChildrenMinimumCapacity);

    public IMdSyntaxNode? Parent { get; set; }

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
    public ReadOnlySpan<IMdSyntaxNode> GetChildrenSpan() {
        return ChildCount != 0
            ? ChildNodes.AsSpan(0, ChildCount)
            : ReadOnlySpan<IMdSyntaxNode>.Empty;
    }

    public void AddChildNode(IMdSyntaxNode childNode) {
        // Check if we need to resize
        EnsureChildNodeCapacity();
        SetParent(childNode);

        // ReSharper disable once HeapView.PossibleBoxingAllocation
        ChildNodes[ChildCount++] = childNode;
    }

    public TChild AddChildNode<TChild>(TChild childNode) where TChild : IMdSyntaxNode {
        // Check if we need to resize
        EnsureChildNodeCapacity();
        SetParent(childNode);

        // ReSharper disable once HeapView.PossibleBoxingAllocation
        ChildNodes[ChildCount++] = childNode;
        return childNode;
    }

    // ReSharper disable once InvertIf
    private void EnsureChildNodeCapacity() {
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

    private void SetParent<TChild>(TChild childNode) where TChild : IMdSyntaxNode {
        childNode.Parent = this;
        childNode.Depth = Depth + 1;
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

    public void ReturnToPool() {
        if (this is not T casted) {
            throw new InvalidOperationException($"Cannot return {GetType()} to shared pool. Expected {typeof(T)}");
        }

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
        return true;
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Quick Alternatives 
// ---------------------------------------------------------------------------------------------------------------------
public abstract class EmptyMdSyntaxNode<T> : MdSyntaxNode<T> where T : MdSyntaxNode<T>, new() {
    protected override IMdSyntaxNode[] ChildNodes { get; set; } = GetInitialChildNodes(0);// Will never have children so don't initialize
}
