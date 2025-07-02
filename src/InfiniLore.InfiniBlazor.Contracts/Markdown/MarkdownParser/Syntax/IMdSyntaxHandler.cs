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

    public void HandleMatch(IMdParserEngine engine, IMdSyntaxNode parentNode, Match entireMatch, Group group, MdSyntaxHandlerOrigin origin);
}
