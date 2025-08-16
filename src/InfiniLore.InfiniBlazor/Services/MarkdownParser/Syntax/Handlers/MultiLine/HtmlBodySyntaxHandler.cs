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
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.HtmlBody)]
public sealed class HtmlBodySyntaxHandler : IMdSyntaxHandler {
    private static readonly int HtmlPreId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlPre);
    private static readonly int HtmlBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlBody);
    private static readonly int HtmlPostId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlPost);
    private static readonly int SpanTagId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanTag);
    private static readonly int SpanTagAttrsId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanTagAttrs);
    private static readonly int SpanBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanBody);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
    ) {
        if (!parentOrigin.HasFlag(MdSyntaxHandlerOrigin.PreserveHtml)) {
            
            parentNode = parentNode.AddChildNode(ParagraphMdSyntaxNode.Pool.Get());
        }

        if (entireMatch.Groups[HtmlPostId].TryGetValue(out string? post)) {
            stack.PushSingleLineMatchesToStack(post, parentNode, parentOrigin);
        }

        if (entireMatch.Groups[HtmlBodyId].TryGetValue(out string? htmlBody)) {
            // Span should be the only special case allowed that allows for Markdown parsing within it
            Match match = MdRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (match.Groups[SpanTagId].TryGetValue(out string? spanTag) && match.Groups[SpanBodyId].TryGetValue(out string? spanBody)) {
                
                
                HtmlSpanMdSyntaxNode spanNode = HtmlSpanMdSyntaxNode.Pool.Get();
                spanNode.TagValue = spanTag;
                
                string spanTagAttrs = match.Groups[SpanTagAttrsId].Value;
                spanNode.Atrributes = spanTagAttrs;
                
                stack.PushMultiLineMatchesToStack(spanBody, spanNode, parentOrigin | MdSyntaxHandlerOrigin.Html);
                stack.PushProcessedNodeToStack(parentNode, spanNode);
            }
            else {
                ContentHtmlMdSyntaxNode htmlNode = ContentHtmlMdSyntaxNode.Pool.Get();
                htmlNode.ContentHtml= htmlBody;
                stack.PushProcessedNodeToStack(parentNode, htmlNode);
            }
        }

        if (entireMatch.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            stack.PushSingleLineMatchesToStack(pre, parentNode, parentOrigin);
        }
    }
}
