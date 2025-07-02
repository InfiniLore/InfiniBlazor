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
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        if (!origin.HasFlag(HandlerOrigin.PreserveHtml)) {
            
            parentNode = parentNode.AddChildNode(ParagraphMdSyntaxNode.Pool.Get());
        }

        if (entireMatch.Groups[HtmlPostId].TryGetValue(out string? post)) {
            engine.PushSingleLineMatchesToStack(post, parentNode, origin);
        }

        if (entireMatch.Groups[HtmlBodyId].TryGetValue(out string? htmlBody)) {
            // Span should be the only special case allowed that allows for Markdown parsing within it
            Match match = MarkdownRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (match.Groups[SpanTagId].TryGetValue(out string? spanTag) && match.Groups[SpanBodyId].TryGetValue(out string? spanBody)) {
                HtmlSpanMdSyntaxNode spanNode = HtmlSpanMdSyntaxNode.Pool.Get();
                spanNode.TagValue = spanTag;
                engine.PushProcessedNodeToStack(parentNode, spanNode);
                engine.PushMultiLineMatchesToStack(spanBody, spanNode, origin | HandlerOrigin.Html);
            }
            else {
                ContentHtmlMdSyntaxNode htmlNode = ContentHtmlMdSyntaxNode.Pool.Get();
                htmlNode.ContentHtml= htmlBody;
                engine.PushProcessedNodeToStack(parentNode, htmlNode);
            }
        }

        if (entireMatch.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            engine.PushSingleLineMatchesToStack(pre, parentNode, origin);
        }
    }
}
