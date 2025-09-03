// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class HtmlSyntaxNodeSerializer  {
    private static readonly int HtmlPreId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlPre);
    private static readonly int HtmlBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlBody);
    private static readonly int HtmlPostId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlPost);
    private static readonly int SpanTagAttrsId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanTagAttrs);
    private static readonly int SpanBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanBody);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        // Only add paragraph wrapper if there's trailing content (pre or post)
        bool hasTrailingContent = match.Groups[HtmlPreId].Success || match.Groups[HtmlPostId].Success;

        Match? spanMatch = null;
        bool hasHtmlBody = match.Groups[HtmlBodyId].TryGetValue(out string? htmlBody);
        string? spanBody = null;
        if (hasHtmlBody && htmlBody is not null) {
            spanMatch = MdRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (spanMatch.Groups[SpanBodyId].TryGetValue(out spanBody)) {
                hasTrailingContent = true;
            }
        }
        
        if (hasTrailingContent && parentNode is not (ParagraphMdSyntaxNode or HtmlSpanMdSyntaxNode)) {
            parentNode = parentNode.AddChildNode(ParagraphMdSyntaxNode.Pool.Get());
        }

        if (match.Groups[HtmlPostId].TryGetValue(out string? post)) {
            stack.PushSingleLineMatchesToStack(post, parentNode);
        }

        if (hasHtmlBody && htmlBody is not null) {
            // Span should be the only special case allowed that allows for Markdown parsing within it
            if (spanMatch is not null && spanBody is not null) {
                HtmlSpanMdSyntaxNode spanNode = HtmlSpanMdSyntaxNode.Pool.Get();

                string spanTagAttrs = spanMatch.Groups[SpanTagAttrsId].Value;
                spanNode.Attributes = spanTagAttrs;

                stack.PushMultiLineMatchesToStack(spanBody, spanNode);
                stack.PushProcessedNodeToStack(parentNode, spanNode);
            }
            else {
                HtmlMdSyntaxNode htmlNode = HtmlMdSyntaxNode.Pool.Get();
                htmlNode.Content = htmlBody;
                stack.PushProcessedNodeToStack(parentNode, htmlNode);
            }
        }

        if (match.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            stack.PushSingleLineMatchesToStack(pre, parentNode);
        }
    }
}
