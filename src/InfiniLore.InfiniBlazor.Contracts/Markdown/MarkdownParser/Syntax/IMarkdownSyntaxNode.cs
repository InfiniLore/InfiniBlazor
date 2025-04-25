// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownSyntaxNode {
    MarkdownElement Element { get; }
    string? Content { get; }
    IMarkdownSyntaxNode Parent { get; }
    int AttributeCount { get; }
    
    ReadOnlySpan<T> GetChildrenSpan<T>(out int length) where T : IMarkdownSyntaxNode;
    ReadOnlySpan<MarkdownAttribute> GetAttributes(out IReadOnlyDictionary<MarkdownAttribute, string> source);

    IMarkdownSyntaxNode AddChildNode(MarkdownElement element, string? content = null);

    IMarkdownSyntaxNode WithContent(string? content);
    IMarkdownSyntaxNode WithHtmlContent(string? content);
    IMarkdownSyntaxNode WithAttribute(MarkdownAttribute attribute, string value);
}
