// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownElementHandler{
    HandlerOrigin SkipOnOrigin { get; }

    public void HandleMatch(IMarkdownParserEngine engine, IMdSyntaxNode parentNode, Match entireMatch, Group group, HandlerOrigin origin);
}
