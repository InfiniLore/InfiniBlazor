// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("italic")]
public class ItalicHandler : IMarkdownElementHandler {

    private static readonly int IId = MarkdownRegexLib.GetSingleLineGroupId("i");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.Italic;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin, CancellationToken ct = default) {
        if (!entireMatch.Groups[IId].TryGetValue(out string? italicValue)) return ValueTask.CompletedTask;

        IMarkdownSyntaxNode node = currentNode.AddChildNode(MarkdownElement.Italic);
        engine.AddSingleLineMatchesToStack(italicValue, node, origin | SkipOnOrigin);
        return ValueTask.CompletedTask;
    }
}
