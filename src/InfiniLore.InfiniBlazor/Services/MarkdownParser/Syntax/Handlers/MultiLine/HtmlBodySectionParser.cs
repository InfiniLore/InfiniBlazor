// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MarkdownRegexGroupNames.HtmlBody)]
public sealed class HtmlBodyHandler : IMdSyntaxHandler {
    private static readonly int HtmlPreId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.HtmlPre);
    private static readonly int HtmlBodyId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.HtmlBody);
    private static readonly int HtmlPostId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.HtmlPost);
    private static readonly int SpanTagId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.SpanTag);
    private static readonly int SpanBodyId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.SpanBody);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        MdSyntaxHandlerOrigin origin
    ) {
        if (!origin.HasFlag(MdSyntaxHandlerOrigin.PreserveHtml)) {
            
            parentNode = parentNode.AddChildNode(ParagraphMdSyntaxNode.Pool.Get());
        }

        if (entireMatch.Groups[HtmlPostId].TryGetValue(out string? post)) {
            stack.PushSingleLineMatchesToStack(post, parentNode, origin);
        }

        if (entireMatch.Groups[HtmlBodyId].TryGetValue(out string? htmlBody)) {
            // Span should be the only special case allowed that allows for Markdown parsing within it
            Match match = MarkdownRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (match.Groups[SpanTagId].TryGetValue(out string? spanTag) && match.Groups[SpanBodyId].TryGetValue(out string? spanBody)) {
                HtmlSpanMdSyntaxNode spanNode = HtmlSpanMdSyntaxNode.Pool.Get();
                spanNode.TagValue = spanTag;
                stack.PushMultiLineMatchesToStack(spanBody, spanNode, origin | MdSyntaxHandlerOrigin.Html);
                stack.PushProcessedNodeToStack(parentNode, spanNode);
            }
            else {
                ContentHtmlMdSyntaxNode htmlNode = ContentHtmlMdSyntaxNode.Pool.Get();
                htmlNode.ContentHtml= htmlBody;
                stack.PushProcessedNodeToStack(parentNode, htmlNode);
            }
        }

        if (entireMatch.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            stack.PushSingleLineMatchesToStack(pre, parentNode, origin);
        }
    }
}
