// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("headingSimple")]
public class HeadingSimpleHandler : IMarkdownElementHandler {

    private static readonly int HsTextId = MarkdownRegexLib.GetMultiLineGroupId("hsText");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[HsTextId].TryGetValue(out string? headerSimpleText)) return;


        IMarkdownSyntaxNode headingElement = currentNode.AddChildNode(MarkdownElement.H1);
        engine.AddSingleLineMatchesToStack(headerSimpleText, headingElement, origin);
    }
}
