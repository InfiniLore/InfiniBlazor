// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNode {
    IMdSyntaxNode? Parent { get; set; }
    int ChildCount { get; }
    int Depth { get; set; }
    Type Type { get; }

    ReadOnlySpan<IMdSyntaxNode> GetChildrenSpan();
    IEnumerable<IMdSyntaxNode> GetChildren();
    IEnumerable<TChild> GetChildrenByType<TChild>() where TChild : IMdSyntaxNode;

    void AddChildNode(IMdSyntaxNode childNode);
    TChild AddChildNode<TChild>(TChild childNode) where TChild : IMdSyntaxNode;

    IMdSyntaxNode WithContent(string content);

    void ReturnToPool();
}
