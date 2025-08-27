// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownStringMdSyntaxNodeSerializer{
    void Serialize(IMdSyntaxFragmentStack engine, IMdSyntaxNode parentNode, Match match);
}
