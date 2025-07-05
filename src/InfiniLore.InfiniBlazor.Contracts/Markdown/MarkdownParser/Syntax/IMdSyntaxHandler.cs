// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxHandler{
    MdSyntaxHandlerOrigin SkipOnOrigin { get; }

    void HandleMatch(IMdSyntaxParserStack engine, IMdSyntaxNode parentNode, Match entireMatch, MdSyntaxHandlerOrigin parentOrigin);
}
