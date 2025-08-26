// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxFragmentStack {
    void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, MdSyntaxSerializerOrigin origin);
    void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node, MdSyntaxSerializerOrigin origin);
    
    void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode);
}
