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
    void PushContentToStack(string content, IMdNode currentNode, ParserOrigin origin);
    void PushElementToStack(string? content, IMdNode currentNode, ParserOrigin origin, MdElement element);
}
