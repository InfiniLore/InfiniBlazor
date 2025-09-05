// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown;
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

    ReadOnlySpan<IMdSyntaxNode> GetChildrenSpan();
    IEnumerable<IMdSyntaxNode> GetChildren();
    IEnumerable<TChild> GetChildrenByType<TChild>() where TChild : IMdSyntaxNode;

    IMdSyntaxNode GetChildAt(int index);
    bool TryGetChildAt(int index, [NotNullWhen(true)] out IMdSyntaxNode? childNode);
    bool TryGetChildAt<TChild>(int index, [NotNullWhen(true)] out TChild? childNode) where TChild : IMdSyntaxNode;

    bool TryGetNextSibling([NotNullWhen(true)] out IMdSyntaxNode? mdSyntaxNode);
    bool HasNextSibling();
    
    void AddChildNode(IMdSyntaxNode childNode);
    TChild AddChildNode<TChild>(TChild childNode) where TChild : IMdSyntaxNode;

    IMdSyntaxNode WithDepth(int depth);
    IMdSyntaxNode WithText(string content);
    IMdSyntaxNode WithParent(IMdSyntaxNode parent);
    IMdSyntaxNode WithModifier(IMdSyntaxNodeModifier modifier);
    IMdSyntaxNode WithChild<TChild>(TChild child) where TChild : IMdSyntaxNode;

    void ReturnToPool();
}
