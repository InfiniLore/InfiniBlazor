// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.ElementHandlers.SingleLine;
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
    public void HandleMatch(IMarkdownParserEngine engine, IMdSyntaxNode parentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[BId].TryGetValue(out string? boldValue)) return;

        BoldMdSyntaxNode node = BoldMdSyntaxNode.Shared.Get();
        parentNode.AddChildNode(node);
        engine.PushSingleLineMatchesToStack(boldValue, node, origin | SkipOnOrigin);
    }
}
