// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxFragmentStack {
    IMdSyntaxTree TreeReference { get; }
    
    void PushMultiLineMatchesToStack(string input, IMdSyntaxNode parentNode, int startIndex = 0);
    void PushSingleLineMatchesToStack(string input, IMdSyntaxNode parentNode);
    
    void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode);
}
