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
[InjectableSingleton<IMarkdownElementHandler>("supScript")]
public class SuperScriptHandler : IMarkdownElementHandler {

    private static readonly int SpId = MarkdownRegexLib.GetSingleLineGroupId("sp");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.SuperScript;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        if (!entireMatch.Groups[SpId].TryGetValue(out string? superValue)) return ;
        
        SuperScriptMdSyntaxNode node = SuperScriptMdSyntaxNode.Shared.Get();
        parentNode.AddChildNode(node);
        engine.PushSingleLineMatchesToStack(superValue, node, origin | SkipOnOrigin);
    }
}
