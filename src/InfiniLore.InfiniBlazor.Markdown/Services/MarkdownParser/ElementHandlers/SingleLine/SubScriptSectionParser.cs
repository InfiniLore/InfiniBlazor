// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("subScript")]
public class SubScriptHandler : IMarkdownElementHandler {

    private static readonly int SbId = MarkdownRegexLib.GetSingleLineGroupId("sb");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.SubScript;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(
        IMarkdownParserEngine engine,
        IMarkdownSyntaxNode currentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin,
        CancellationToken ct = default
    ) {
        if (!entireMatch.Groups[SbId].TryGetValue(out string? subValue)) return ValueTask.CompletedTask;

        IMarkdownSyntaxNode node = currentNode.AddChildNode(MarkdownElement.Subscript);
        engine.AddSingleLineMatchesToStack(subValue, node, origin | SkipOnOrigin);
        return ValueTask.CompletedTask;
    }
}
