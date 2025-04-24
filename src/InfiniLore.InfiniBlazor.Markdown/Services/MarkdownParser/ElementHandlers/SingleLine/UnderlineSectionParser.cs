// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("underline")]
public class UnderlineHandler : IMarkdownElementHandler {

    private static readonly int UId = MarkdownRegexLib.GetSingleLineGroupId("u");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.Underline;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[UId].TryGetValue(out string? underlineValue)) return;

        IMdNode underlineNode = currentNode.AddChildNode(MdElement.Underline);
        engine.AddSingleLineMatchesToStack(underlineValue, underlineNode, origin | SkipOnOrigin);
    }
}
