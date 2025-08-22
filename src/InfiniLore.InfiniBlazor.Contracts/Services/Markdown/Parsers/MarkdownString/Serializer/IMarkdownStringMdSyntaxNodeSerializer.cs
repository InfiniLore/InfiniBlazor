// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownStringMdSyntaxNodeSerializer{
    MdSyntaxSerializerOrigin SkipOnOrigin { get; }

    void HandleMatch(IMdSyntaxFragmentStack engine, IMdSyntaxNode parentNode, Match entireMatch, MdSyntaxSerializerOrigin parentOrigin);
}
