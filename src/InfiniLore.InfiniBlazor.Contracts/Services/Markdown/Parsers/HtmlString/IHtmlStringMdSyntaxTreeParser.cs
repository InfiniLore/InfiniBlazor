// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IHtmlStringMdSyntaxTreeParser {
    // IMdSyntaxTree SerializeToSyntaxTree(string input); // plain HTML to syntax tree is not supported 
    string DeserializeToString(IMdSyntaxTree tree);
}
