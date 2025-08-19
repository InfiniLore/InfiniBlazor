// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer.NodeSerializers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.HtmlBody)]
public sealed class HtmlBodySyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int HtmlPreId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlPre);
    private static readonly int HtmlBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlBody);
    private static readonly int HtmlPostId = MdRegexLib.GetGroupId(MdRegexGroupNames.HtmlPost);
    private static readonly int SpanTagId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanTag);
    private static readonly int SpanTagAttrsId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanTagAttrs);
    private static readonly int SpanBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.SpanBody);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
    ) {
        if (!parentOrigin.HasFlag(MdSyntaxSerializerOrigin.PreserveHtml)) {
            
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
                
                stack.PushMultiLineMatchesToStack(spanBody, spanNode, parentOrigin | MdSyntaxSerializerOrigin.Html);
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
