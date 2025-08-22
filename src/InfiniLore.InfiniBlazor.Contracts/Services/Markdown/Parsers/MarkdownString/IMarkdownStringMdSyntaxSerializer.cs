// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownStringMdSyntaxSerializer {
    IMdSyntaxTree SerializeToTree(string markdown);
    void SerializeToTree(string markdown, IMdSyntaxTree nodeTree);
}
