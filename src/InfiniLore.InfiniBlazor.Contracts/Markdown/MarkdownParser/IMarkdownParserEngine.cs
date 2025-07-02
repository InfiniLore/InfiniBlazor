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
    void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, HandlerOrigin origin);
    void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node, HandlerOrigin origin);
    
    void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode);
}
