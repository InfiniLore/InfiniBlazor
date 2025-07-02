// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.ElementHandlers.MultiLine;
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
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        if (!entireMatch.Groups[PId].TryGetValue(out string? paragraph)) return;
        if (paragraph.IsNullOrWhiteSpace()) return;

        bool writeParagraph = !origin.HasFlag(HandlerOrigin.Html);
        
        if (writeParagraph) {
            ParagraphMdSyntaxNode node = ParagraphMdSyntaxNode.Shared.Get();
            parentNode = parentNode.AddChildNode(node);
        }
        engine.PushSingleLineMatchesToStack(paragraph.TrimStart(), parentNode, origin);
    }
}
