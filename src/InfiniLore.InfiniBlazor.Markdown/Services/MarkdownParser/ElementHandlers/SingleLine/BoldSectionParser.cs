// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("bold")]
public class BoldHandler : IMarkdownElementHandler {

    private static readonly int BId = MarkdownRegexLib.GetSingleLineGroupId("b");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.Bold;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[BId].TryGetValue(out string? boldValue)) return;

        IMdNode boldNode = currentNode.AddChildNode(MdElement.Bold);
        engine.AddSingleLineMatchesToStack(boldValue, boldNode, origin | SkipOnOrigin);
    }
}
