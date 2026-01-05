// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class HtmlBlockSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex("""
        (?<pre>.+?)?
        (?<body>
            <(?<tag>\w+)\b[^>]*>
            (?>
                [^<]+
                | <(?<open>\k<tag>)\b[^>]*>
                | </(?<-open>\k<tag>)>
                | <(?!/?\k<tag>\b)[^>]+>
            )*
            (?(open)(?!))
            (</\k<tag>>)
        )
        (?<post>.+)?
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    [GeneratedRegex("""
        <span\ ?(?<attr>\b[^>]*)>
        (?<body>
          (?>
            [^<]+
            | <(?<open>span)\b[^>]*>
            | </(?<-open>span)>
            | <(?!/?span\b)[^>]+>
          )*
        )
        (?(open)(?!))
        (</span>)
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.Compiled)]
    private static partial Regex SpanSyntax { get; }
    
    private static readonly int HtmlPreId = Syntax.GroupNumberFromName("pre");
    private static readonly int HtmlBodyId = Syntax.GroupNumberFromName("body");
    private static readonly int HtmlPostId = Syntax.GroupNumberFromName("post");
    private static readonly int SpanTagAttrsId = SpanSyntax.GroupNumberFromName("attr");
    private static readonly int SpanBodyId = SpanSyntax.GroupNumberFromName("body");
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
