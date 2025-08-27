// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.HtmlBody)]
public sealed class HtmlBodySyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int HtmlPreId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlPre);
    private static readonly int HtmlBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlBody);
    private static readonly int HtmlPostId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlPost);
    private static readonly int SpanTagId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanTag);
    private static readonly int SpanTagAttrsId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanTagAttrs);
    private static readonly int SpanBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanBody);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch
    ) {
        // Only add paragraph wrapper if there's trailing content (pre or post)
        bool hasTrailingContent = entireMatch.Groups[HtmlPreId].Success || entireMatch.Groups[HtmlPostId].Success;

        Match? spanMatch = null;
        bool hasHtmlBody = entireMatch.Groups[HtmlBodyId].TryGetValue(out string? htmlBody);
        string? spanTag = null;
        string? spanBody = null;
        if (hasHtmlBody && htmlBody is not null) {
            spanMatch = MdRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (spanMatch.Groups[SpanTagId].TryGetValue(out spanTag) && spanMatch.Groups[SpanBodyId].TryGetValue(out spanBody)) {
                hasTrailingContent = true;
            }
        }
        
        if (hasTrailingContent && parentNode is not (ParagraphMdSyntaxNode or HtmlSpanMdSyntaxNode)) {
            parentNode = parentNode.AddChildNode(ParagraphMdSyntaxNode.Pool.Get());
        }

        if (entireMatch.Groups[HtmlPostId].TryGetValue(out string? post)) {
            stack.PushSingleLineMatchesToStack(post, parentNode);
        }

        if (hasHtmlBody && htmlBody is not null) {
            // Span should be the only special case allowed that allows for Markdown parsing within it
            if (spanMatch is not null && spanTag is not null && spanBody is not null) {
                HtmlSpanMdSyntaxNode spanNode = HtmlSpanMdSyntaxNode.Pool.Get();
                spanNode.TagValue = spanTag;

                string spanTagAttrs = spanMatch.Groups[SpanTagAttrsId].Value;
                spanNode.Attributes = spanTagAttrs;

                stack.PushMultiLineMatchesToStack(spanBody, spanNode);
                stack.PushProcessedNodeToStack(parentNode, spanNode);
            }
            else {
                ContentHtmlMdSyntaxNode htmlNode = ContentHtmlMdSyntaxNode.Pool.Get();
                htmlNode.ContentHtml = htmlBody;
                stack.PushProcessedNodeToStack(parentNode, htmlNode);
            }
        }

        if (entireMatch.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            stack.PushSingleLineMatchesToStack(pre, parentNode);
        }
    }
}
