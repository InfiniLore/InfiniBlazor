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
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[SbId].TryGetValue(out string? subValue)) return;

        IMdNode node = currentNode.AddChildNode(MdElement.Subscript);
        engine.AddSingleLineMatchesToStack(subValue, node, origin | SkipOnOrigin);
    }
}
