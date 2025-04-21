// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IRunningMarkdownParser {
    IMdNode RootNode { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    void AddMultiLineMatchesToStack(string input, IMdNode node, ParserOrigin origin);
    void AddSingleLineMatchesToStack(string input, IMdNode node, ParserOrigin origin);
    void PushContentToStack(string span, IMdNode currentNode, ParserOrigin origin);
    void PushHtmlContentToStack(string span, IMdNode currentNode, ParserOrigin origin);
}
