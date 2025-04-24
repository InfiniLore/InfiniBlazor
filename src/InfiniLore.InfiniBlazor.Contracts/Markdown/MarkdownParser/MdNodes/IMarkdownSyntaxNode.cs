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
    IReadOnlyDictionary<string, string> Attributes { get; }
    IReadOnlySet<string> Classes { get; }
    IMarkdownSyntaxNode Parent { get; }
    
    ReadOnlySpan<T> GetChildrenSpan<T>(out int length) where T : IMarkdownSyntaxNode;

    IMarkdownSyntaxNode AddChildNode(MarkdownElement element, string? content = null);

    IMarkdownSyntaxNode WithContent(string? content);
    IMarkdownSyntaxNode WithHtmlContent(string? content);
    IMarkdownSyntaxNode WithClass(string className);
    IMarkdownSyntaxNode WithAttribute(string key, string value);
}
