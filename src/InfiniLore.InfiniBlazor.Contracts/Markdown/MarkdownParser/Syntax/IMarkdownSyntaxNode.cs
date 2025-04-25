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
    
    ReadOnlySpan<T> GetChildrenSpan<T>(out int length) where T : IMarkdownSyntaxNode;
    
    bool TryGetAttributes(out int count, out ReadOnlySpan<MarkdownAttribute> attributes, out ReadOnlySpan<string> sources);

    IMarkdownSyntaxNode AddChildNode(MarkdownElement element, string? content = null);

    IMarkdownSyntaxNode WithContent(string? content);
    IMarkdownSyntaxNode WithHtmlContent(string? content);
    IMarkdownSyntaxNode WithAttribute(MarkdownAttribute attribute, string value);
}
