// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNodeSerializer{
    MdSyntaxSerializerOrigin SkipOnOrigin { get; }

    void HandleMatch(IMdSyntaxSerializerStack engine, IMdSyntaxNode parentNode, Match entireMatch, MdSyntaxSerializerOrigin parentOrigin);
}
