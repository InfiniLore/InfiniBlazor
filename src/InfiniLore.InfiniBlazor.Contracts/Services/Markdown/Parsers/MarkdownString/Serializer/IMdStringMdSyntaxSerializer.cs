// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdStringMdSyntaxSerializer {
    IMdSyntaxTree SerializeToTree(string markdown);
    void SerializeToTree(string markdown, IMdSyntaxTree nodeTree);
}
