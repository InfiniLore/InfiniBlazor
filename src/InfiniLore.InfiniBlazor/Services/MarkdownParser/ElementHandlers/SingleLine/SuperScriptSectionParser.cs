// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.ElementHandlers.SingleLine;
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
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMarkdownSyntaxNode currentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        if (!entireMatch.Groups[SpId].TryGetValue(out string? superValue)) return ;

        IMarkdownSyntaxNode node = currentNode.AddChildNode(MarkdownElement.Superscript);
        engine.AddSingleLineMatchesToStack(superValue, node, origin | SkipOnOrigin);
    }
}
