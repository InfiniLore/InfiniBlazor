// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxFragmentStack {
    IMdSyntaxTree TreeReference { get; }
    
    void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, int startIndex = 0);
    void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node);
    
    void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode);
}
