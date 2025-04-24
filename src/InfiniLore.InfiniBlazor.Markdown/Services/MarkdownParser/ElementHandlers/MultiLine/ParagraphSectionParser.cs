// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("paragraph")]
public class ParagraphHandler : IMarkdownElementHandler {

    private static readonly int PId = MarkdownRegexLib.GetSingleLineGroupId("p");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[PId].TryGetValue(out string? paragraph)) return;
        if (paragraph.IsNullOrWhiteSpace()) return;

        bool writeParagraph = !origin.HasFlag(HandlerOrigin.Html);

        if (writeParagraph) currentNode = currentNode.AddChildNode(MarkdownElement.Paragraph);
        engine.AddSingleLineMatchesToStack(paragraph.TrimStart(), currentNode, origin);
    }
}
