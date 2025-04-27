// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("strike")]
public class StrikeHandler : IMarkdownElementHandler {

    private static readonly int SId = MarkdownRegexLib.GetSingleLineGroupId("s");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.Strike;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin, CancellationToken ct = default) {
        if (!entireMatch.Groups[SId].TryGetValue(out string? strikeValue)) return ValueTask.CompletedTask;

        IMarkdownSyntaxNode node = currentNode.AddChildNode(MarkdownElement.Strikethrough);
        engine.AddSingleLineMatchesToStack(strikeValue, node, origin | SkipOnOrigin);
        return ValueTask.CompletedTask;
    }
}
