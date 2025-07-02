// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNode {
    IMdSyntaxNode? Parent { get; }
    int ChildCount { get; }

    ReadOnlySpan<IMdSyntaxNode> GetChildrenSpan();

    void AddChildNode(IMdSyntaxNode childNode);
    TChild AddChildNode<TChild>(TChild childNode) where TChild : IMdSyntaxNode;

    IMdSyntaxNode WithContent(string content);

    void ReturnToShared();
}
