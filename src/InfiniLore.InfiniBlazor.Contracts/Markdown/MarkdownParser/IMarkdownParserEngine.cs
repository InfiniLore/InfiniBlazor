// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownParserEngine {
    IMdNodeTree NodeTree { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    void AddMultiLineMatchesToStack(string input, IMdNode node, HandlerOrigin origin);
    void AddSingleLineMatchesToStack(string input, IMdNode node, HandlerOrigin origin);
    void PushContentToStack(string content, IMdNode currentNode, HandlerOrigin origin);
    void PushElementToStack(string? content, IMdNode currentNode, HandlerOrigin origin, MdElement element);
}
