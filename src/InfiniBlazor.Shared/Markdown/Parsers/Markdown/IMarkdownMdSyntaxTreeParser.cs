// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;

namespace InfiniBlazor.Markdown.Parsers.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownMdSyntaxTreeParser {
    IMdSyntaxTree SerializeToSyntaxTree(string input);
    string DeserializeToString(IMdSyntaxTree tree);
}
