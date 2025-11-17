// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdStringMdSyntaxSerializer {
    IMdSyntaxTree SerializeToTree(string markdown);
    void SerializeToTree(string markdown, IMdSyntaxTree nodeTree);
}
