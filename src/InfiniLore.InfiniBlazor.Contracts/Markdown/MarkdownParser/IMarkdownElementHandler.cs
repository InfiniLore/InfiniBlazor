// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
internal interface IMarkdownElementHandler{
    HandlerOrigin SkipOnOrigin { get; }

    public ValueTask HandleMatchAsync(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin, CancellationToken ct = default);
}
