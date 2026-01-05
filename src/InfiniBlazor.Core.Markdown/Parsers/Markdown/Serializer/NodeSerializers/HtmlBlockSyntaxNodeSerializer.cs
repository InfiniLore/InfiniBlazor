// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class HtmlBlockSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex("""
        (?:
            (?<htmlPre>.+?)?
            (?<htmlBody>
                <(?<htmlTag>\w+)\b[^>]*>
                (?>
                    [^<]+
                    | <(?<open>\k<htmlTag>)\b[^>]*>
                    | </(?<-open>\k<htmlTag>)>
                    | <(?!/?\k<htmlTag>\b)[^>]+>
                )*
                (?(open)(?!))
                (</\k<htmlTag>>)
            )
            (?<htmlPost>.+)?
        )
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    [GeneratedRegex("""
        (?<spanTag><(?<spanHtmlTag>span)\ ?(?<spanTagAttrs>\b[^>]*)>)
        (?<spanBody>
          (?>
            [^<]+
            | <(?<open>\k<spanHtmlTag>)\b[^>]*>
            | </(?<-open>\k<spanHtmlTag>)>
            | <(?!/?\k<spanHtmlTag>\b)[^>]+>
          )*
        )
        (?(open)(?!))
        (</\k<spanHtmlTag>>)
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.Compiled)]
    private static partial Regex SpanSyntax { get; }
    
    private static readonly int HtmlPreId = Syntax.GroupNumberFromName(MdRegexGroupNames.HtmlPre);
    private static readonly int HtmlBodyId = Syntax.GroupNumberFromName(MdRegexGroupNames.HtmlBody);
    private static readonly int HtmlPostId = Syntax.GroupNumberFromName(MdRegexGroupNames.HtmlPost);
    private static readonly int SpanTagAttrsId = SpanSyntax.GroupNumberFromName(MdRegexGroupNames.SpanTagAttrs);
    private static readonly int SpanBodyId = SpanSyntax.GroupNumberFromName(MdRegexGroupNames.SpanBody);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        // Only add a paragraph wrapper if there's trailing content (pre or post)
        bool hasTrailingContent = match.Groups[HtmlPreId].Success || match.Groups[HtmlPostId].Success;

        Match? spanMatch = null;
        bool hasHtmlBody = match.Groups[HtmlBodyId].TryGetValue(out string? htmlBody);
        string? spanBody = null;
        if (hasHtmlBody && htmlBody is not null) {
            spanMatch = SpanSyntax.Match(htmlBody);
            if (spanMatch.Groups[SpanBodyId].TryGetValue(out spanBody)) {
                hasTrailingContent = true;
            }
        }

        if (hasTrailingContent && parentNode is not (ParagraphMdSyntaxNode or HtmlSpanMdSyntaxNode)) {
            parentNode = parentNode.AddChildNode(MdSyntaxNodePool<ParagraphMdSyntaxNode>.Shared.Get());
        }

        if (match.Groups[HtmlPostId].TryGetValue(out string? post)) {
            stack.PushSingleLineMatchesToStack(post, parentNode);
        }

        if (hasHtmlBody && htmlBody is not null) {
            // Span should be the only special case allowed that allows for Markdown parsing within it
            if (spanMatch is not null && spanBody is not null) {
                HtmlSpanMdSyntaxNode spanNode = MdSyntaxNodePool<HtmlSpanMdSyntaxNode>.Shared.Get();

                string spanTagAttrs = spanMatch.Groups[SpanTagAttrsId].Value;
                spanNode.WithAttributes(spanTagAttrs);

                stack.PushMultiLineMatchesToStack(spanBody, spanNode);
                stack.PushProcessedNodeToStack(parentNode, spanNode);
            }
            else {
                HtmlMdSyntaxNode htmlNode = MdSyntaxNodePool<HtmlMdSyntaxNode>.Shared.Get();
                htmlNode.WithContent(htmlBody);
                stack.PushProcessedNodeToStack(parentNode, htmlNode);
            }
        }

        if (match.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            stack.PushSingleLineMatchesToStack(pre, parentNode);
        }
    }
}
