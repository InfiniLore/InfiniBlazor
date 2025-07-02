// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.ElementHandlers.SingleLine;
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
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMarkdownSyntaxNode currentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        if (!entireMatch.Groups[SId].TryGetValue(out string? strikeValue)) return ;

        IMarkdownSyntaxNode node = currentNode.AddChildNode(MarkdownElement.Strikethrough);
        engine.AddSingleLineMatchesToStack(strikeValue, node, origin | SkipOnOrigin);
    }
}
