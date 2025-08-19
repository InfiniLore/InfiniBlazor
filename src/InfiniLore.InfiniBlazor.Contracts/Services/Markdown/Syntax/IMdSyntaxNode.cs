// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNode {
    IMdSyntaxNode? Parent { get; }
    int ChildCount { get; }
    int Depth { get; }
    Type Type { get; }
    [MemberNotNullWhen(true, nameof(Modifiers))] bool ContainsModifiers { get; }
    IMdSyntaxNodeModifier? Modifiers { get; set; }

    ReadOnlySpan<IMdSyntaxNode> GetChildrenSpan();
    IEnumerable<IMdSyntaxNode> GetChildren();
    IEnumerable<TChild> GetChildrenByType<TChild>() where TChild : IMdSyntaxNode;

    IMdSyntaxNode GetChildAt(int index);
    TChild GetChildAt<TChild>(int index) where TChild : IMdSyntaxNode;
    bool TryGetChildAt(int index, [NotNullWhen(true)] out IMdSyntaxNode? childNode);
    bool TryGetChildAt<TChild>(int index, [NotNullWhen(true)] out TChild? childNode) where TChild : IMdSyntaxNode;

    void AddChildNode(IMdSyntaxNode childNode);
    TChild AddChildNode<TChild>(TChild childNode) where TChild : IMdSyntaxNode;

    IMdSyntaxNode WithContent(string content);
    IMdSyntaxNode WithDepth(int depth);
    IMdSyntaxNode WithParent(IMdSyntaxNode parent);

    void ReturnToPool();
}
