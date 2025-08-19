// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxSerializerStack {
    void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, MdSyntaxSerializerOrigin origin);
    void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node, MdSyntaxSerializerOrigin origin);
    
    void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode);
}
