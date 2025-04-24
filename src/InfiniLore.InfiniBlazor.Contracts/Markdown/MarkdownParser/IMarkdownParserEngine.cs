// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownParserEngine {
    IMarkdownSyntaxTree NodeTree { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    void AddMultiLineMatchesToStack(string input, IMarkdownSyntaxNode node, HandlerOrigin origin);
    void AddSingleLineMatchesToStack(string input, IMarkdownSyntaxNode node, HandlerOrigin origin);
    void PushContentToStack(string content, IMarkdownSyntaxNode currentNode, HandlerOrigin origin);
    void PushElementToStack(string? content, IMarkdownSyntaxNode currentNode, HandlerOrigin origin, MarkdownElement element);
}
