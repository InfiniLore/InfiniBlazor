// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNode : IResettable, IEquatable<IMdSyntaxNode>{
    Guid Id { get; }
    IMdSyntaxNode? Parent { get; }
    int ChildCount { get; }
    int Depth { get; }
    Type Type { get; }
    IMdSyntaxNodeModifier? Modifier { get; }
    IMdSyntaxTree TreeReference { get; }

    ReadOnlySpan<IMdSyntaxNode> GetChildrenSpan();
    IEnumerable<IMdSyntaxNode> GetChildren();
    IEnumerable<TChild> GetChildrenByType<TChild>() where TChild : IMdSyntaxNode;

    IMdSyntaxNode GetChildAt(int index);
    bool TryGetChildAt(int index, [NotNullWhen(true)] out IMdSyntaxNode? childNode);
    bool TryGetChildAt<TChild>(int index, [NotNullWhen(true)] out TChild? childNode) where TChild : IMdSyntaxNode;

    bool TryGetNextSibling([NotNullWhen(true)] out IMdSyntaxNode? mdSyntaxNode);
    bool HasNextSibling();
    
    bool TryGetPreviousSibling([NotNullWhen(true)] out IMdSyntaxNode? mdSyntaxNode);
    bool HasPreviousSibling();
    
    int GetIndexAtParent();
    
    /// <summary>
    /// Returns a simple string representation of the node for tree visualization.
    /// </summary>
    string ToDebugString();
    
    void AddChildNode(IMdSyntaxNode childNode);
    TChild AddChildNode<TChild>(TChild childNode) where TChild : IMdSyntaxNode;

    IMdSyntaxNode WithDepth(int depth);
    IMdSyntaxNode WithText(string content);
    IMdSyntaxNode WithParent(IMdSyntaxNode parent);
    IMdSyntaxNode WithModifier(IMdSyntaxNodeModifier modifier);
    IMdSyntaxNode WithChild<TChild>(TChild child) where TChild : IMdSyntaxNode;
    
    internal void ReturnToPool();
}
