// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("supScript")]
public class SuperScriptHandler : IMarkdownElementHandler {

    private static readonly int SpId = MarkdownRegexLib.GetSingleLineGroupId("sp");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.SuperScript;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[SpId].TryGetValue(out string? superValue)) return;

        IMdNode node = currentNode.AddChildNode(MdElement.Superscript);
        engine.AddSingleLineMatchesToStack(superValue, node, origin | SkipOnOrigin);
    }
}
