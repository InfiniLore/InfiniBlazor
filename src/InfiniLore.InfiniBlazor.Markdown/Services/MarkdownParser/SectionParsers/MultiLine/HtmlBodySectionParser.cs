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

    private static readonly int HtmlPreId = MarkdownRegexLib.GetMultiLineGroupId("htmlPre");
    private static readonly int HtmlBodyId = MarkdownRegexLib.GetMultiLineGroupId("htmlBody");
    private static readonly int HtmlPostId = MarkdownRegexLib.GetMultiLineGroupId("htmlPost");
    private static readonly int SpanTagId = MarkdownRegexLib.GetSpanGroupId("spanTag");
    private static readonly int SpanBodyId = MarkdownRegexLib.GetSpanGroupId("spanBody");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!origin.HasFlag(ParserOrigin.PreserveHtml)) {
            currentNode = currentNode.AddChildNode(MdElement.Paragraph);
        }

        if (entireMatch.Groups[HtmlPostId].TryGetValue(out string? post)) {
            engine.AddSingleLineMatchesToStack(post, currentNode, origin);
        }

        if (entireMatch.Groups[HtmlBodyId].TryGetValue(out string? htmlBody)) {
            // Span should be the only special case allowed that allows for Markdown parsing within it
            Match match = MarkdownRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (match.Groups[SpanTagId].TryGetValue(out string? spanTag) && match.Groups[SpanBodyId].TryGetValue(out string? spanBody)) {
                engine.PushElementToStack("</span>", currentNode, origin, MdElement.HtmlContent);
                engine.AddMultiLineMatchesToStack(spanBody, currentNode, origin | ParserOrigin.Html);
                engine.PushElementToStack(spanTag, currentNode, origin, MdElement.HtmlContent);
            }
            else {
                engine.PushElementToStack(htmlBody, currentNode, origin, MdElement.HtmlContent);
            }
        }

        if (entireMatch.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            engine.AddSingleLineMatchesToStack(pre, currentNode, origin);
        }
    }
}
