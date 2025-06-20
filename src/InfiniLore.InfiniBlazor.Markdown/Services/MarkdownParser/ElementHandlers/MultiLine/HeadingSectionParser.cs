// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("heading")]
public class HeadingHandler : IMarkdownElementHandler {

    private static readonly int HLevelId = MarkdownRegexLib.GetMultiLineGroupId("hLevel");
    private static readonly int HTextId = MarkdownRegexLib.GetMultiLineGroupId("hText");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;
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
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!entireMatch.Groups[HLevelId].TryGetLength(out int headingLevel)) return;
        if (!entireMatch.Groups[HTextId].TryGetValue(out string? headerText)) return;

        MarkdownElement mdElement = headingLevel switch {
            1 => MarkdownElement.H1,
            2 => MarkdownElement.H2,
            3 => MarkdownElement.H3,
            4 => MarkdownElement.H4,
            5 => MarkdownElement.H5,
            6 => MarkdownElement.H6,
            _ => MarkdownElement.H1
        };

        IMarkdownSyntaxNode headingElement = currentNode.AddChildNode(mdElement);
        engine.AddSingleLineMatchesToStack(headerText, headingElement, origin);
    }
}
