// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownMdSyntaxTreeParser {
    IMdSyntaxTree SerializeToSyntaxTree(string input);
    string DeserializeToString(IMdSyntaxTree tree);
}
