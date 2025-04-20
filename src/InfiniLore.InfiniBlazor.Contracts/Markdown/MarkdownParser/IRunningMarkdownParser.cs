// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

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
    void AddStringToStack(string value, IMdNode node, ParserOrigin origin);
}
