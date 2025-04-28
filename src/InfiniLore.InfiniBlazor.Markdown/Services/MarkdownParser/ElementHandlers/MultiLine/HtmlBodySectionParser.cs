// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("htmlBody")]
public class HtmlBodyHandler : IMarkdownElementHandler {

    private static readonly int HtmlPreId = MarkdownRegexLib.GetMultiLineGroupId("htmlPre");
    private static readonly int HtmlBodyId = MarkdownRegexLib.GetMultiLineGroupId("htmlBody");
    private static readonly int HtmlPostId = MarkdownRegexLib.GetMultiLineGroupId("htmlPost");
    private static readonly int SpanTagId = MarkdownRegexLib.GetSpanGroupId("spanTag");
    private static readonly int SpanBodyId = MarkdownRegexLib.GetSpanGroupId("spanBody");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(
        IMarkdownParserEngine engine,
        IMarkdownSyntaxNode currentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin,
        CancellationToken ct = default
    ) {
        if (!origin.HasFlag(HandlerOrigin.PreserveHtml)) {
            currentNode = currentNode.AddChildNode(MarkdownElement.Paragraph);
        }

        if (entireMatch.Groups[HtmlPostId].TryGetValue(out string? post)) {
            engine.AddSingleLineMatchesToStack(post, currentNode, origin);
        }

        if (entireMatch.Groups[HtmlBodyId].TryGetValue(out string? htmlBody)) {
            // Span should be the only special case allowed that allows for Markdown parsing within it
            Match match = MarkdownRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (match.Groups[SpanTagId].TryGetValue(out string? spanTag) && match.Groups[SpanBodyId].TryGetValue(out string? spanBody)) {
                engine.PushElementToStack("</span>", currentNode, origin, MarkdownElement.HtmlContent);
                engine.AddMultiLineMatchesToStack(spanBody, currentNode, origin | HandlerOrigin.Html);
                engine.PushElementToStack(spanTag, currentNode, origin, MarkdownElement.HtmlContent);
            }
            else {
                engine.PushElementToStack(htmlBody, currentNode, origin, MarkdownElement.HtmlContent);
            }
        }

        if (entireMatch.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            engine.AddSingleLineMatchesToStack(pre, currentNode, origin);
        }
        return ValueTask.CompletedTask;
    }
}
