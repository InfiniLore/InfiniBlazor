// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Ganss.Xss;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("htmlBody")]
public class HtmlBodySectionParser(IHtmlSanitizer htmlSanitizer, ICachedRegexGroupNames groupName) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private readonly int HtmlPreId = groupName.GetMultiLineGroupId("htmlPre");
    private readonly int HtmlBodyId = groupName.GetMultiLineGroupId("htmlBody");
    private readonly int HtmlPostId = groupName.GetMultiLineGroupId("htmlPost");
    private readonly int SpanTagId = groupName.GetSpanGroupId("spanTag");
    private readonly int SpanBodyId = groupName.GetSpanGroupId("spanBody");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group group, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        bool writeParagraph = !origin.HasFlag(ParserOrigin.PreserveHtml);
        if (writeParagraph) {
            currentNode = currentNode.AddChild(MdElement.Paragraph);
        }
        if (entireMatch.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            parser.AddSingleLineMatchesToStack(pre, currentNode, origin);
        }

        if (entireMatch.Groups[HtmlBodyId].TryGetValue(out string? htmlBody)) {
            // Span should be only special case allowed which allows for markdown parsing within it
            Match match = MarkdownRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (match.Groups[SpanTagId].TryGetValue(out string? spanTag) && match.Groups[SpanBodyId].TryGetValue(out string? spanBody)) {
                currentNode.WithContent(htmlSanitizer.Sanitize(spanTag).AsSpan()[..^7].ToString());
                parser.AddMultiLineMatchesToStack(spanBody, currentNode, origin | ParserOrigin.Html);
            }
            else {
                currentNode.WithContent(htmlSanitizer.Sanitize(htmlBody));
            }
        }

        if (entireMatch.Groups[HtmlPostId].TryGetValue(out string? post)) {
            parser.AddSingleLineMatchesToStack(post, currentNode, origin);
        }
    }
}
