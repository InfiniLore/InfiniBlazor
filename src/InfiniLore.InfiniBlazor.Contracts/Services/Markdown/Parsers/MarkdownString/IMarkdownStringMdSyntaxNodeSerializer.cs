// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownStringMdSyntaxNodeSerializer{
    MarkdownStringMdSyntaxSerializerOrigin SkipOnOrigin { get; }

    void HandleMatch(IMarkdownStringMdSyntaxSerializerStack engine, IMdSyntaxNode parentNode, Match entireMatch, MarkdownStringMdSyntaxSerializerOrigin parentOrigin);
}
