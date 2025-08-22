// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownStringMdSyntaxSerializerStack {
    void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, MarkdownStringMdSyntaxSerializerOrigin origin);
    void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node, MarkdownStringMdSyntaxSerializerOrigin origin);
    
    void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode);
}
