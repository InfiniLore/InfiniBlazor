// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("htmlBody")]
public class HtmlBodySectionParser : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private static readonly int HtmlPreId = CachedRegexGroupNames.GetMultiLineGroupId("htmlPre");
    private static readonly int HtmlBodyId = CachedRegexGroupNames.GetMultiLineGroupId("htmlBody");
    private static readonly int HtmlPostId = CachedRegexGroupNames.GetMultiLineGroupId("htmlPost");
    private static readonly int SpanTagId = CachedRegexGroupNames.GetSpanGroupId("spanTag");
    private static readonly int SpanBodyId = CachedRegexGroupNames.GetSpanGroupId("spanBody");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!origin.HasFlag(ParserOrigin.PreserveHtml)) {
            currentNode = currentNode.AddChildNode(MdElement.Paragraph);
        }
        
        if (entireMatch.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            parser.AddSingleLineMatchesToStack(pre, currentNode, origin);
        }

        if (entireMatch.Groups[HtmlBodyId].TryGetValue(out string? htmlBody)) {
            // Span should be the only special case allowed that allows for Markdown parsing within it
            Match match = MarkdownRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (match.Groups[SpanTagId].TryGetValue(out string? spanTag) && match.Groups[SpanBodyId].TryGetValue(out string? spanBody)) {
                currentNode.WithHtmlContent((spanTag));
                parser.AddMultiLineMatchesToStack(spanBody, currentNode, origin | ParserOrigin.Html);
                parser.PushContentToStack("</span>", currentNode, origin);
            }
            else {
                currentNode.WithHtmlContent(htmlBody);
            }
        }

        if (entireMatch.Groups[HtmlPostId].TryGetValue(out string? post)) {
            parser.AddSingleLineMatchesToStack(post, currentNode, origin);
        }
    }
}
