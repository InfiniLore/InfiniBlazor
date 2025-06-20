// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("tag")]
public class TagHandler : IMarkdownElementHandler {

    private static readonly int TextId = MarkdownRegexLib.GetSingleLineGroupId("tText");
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
        if (!entireMatch.Groups[TextId].TryGetValue(out string? tagValue)) return ;

        IMarkdownSyntaxNode spanNode = currentNode.AddChildNode(MarkdownElement.Tag);
        spanNode.WithContent(tagValue);
    }
}
