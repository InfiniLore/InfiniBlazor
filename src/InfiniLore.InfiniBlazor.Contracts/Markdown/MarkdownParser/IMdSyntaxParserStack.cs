// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxParserStack {
    void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, MdSyntaxHandlerOrigin origin);
    void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node, MdSyntaxHandlerOrigin origin);
    
    void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode);
}
