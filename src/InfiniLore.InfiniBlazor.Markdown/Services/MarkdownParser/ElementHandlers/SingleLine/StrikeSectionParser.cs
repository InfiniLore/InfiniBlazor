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
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[SId].TryGetValue(out string? strikeValue)) return;

        IMdNode node = currentNode.AddChildNode(MdElement.Strikethrough);
        engine.AddSingleLineMatchesToStack(strikeValue, node, origin | SkipOnOrigin);
    }
}
