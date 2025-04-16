// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Ganss.Xss;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Services.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("htmlBody")]
public class HtmlBodySectionParser(IServiceProvider provider, IHtmlSanitizer htmlSanitizer, ICachedRegexGroupNames groupName) : IMultiLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    
    private readonly int HtmlPreId = groupName.GetMultiLineGroupId("htmlPre");
    private readonly int HtmlBodyId = groupName.GetMultiLineGroupId("htmlBody");
    private readonly int HtmlPostId = groupName.GetMultiLineGroupId("htmlPost");
    private readonly int SpanTagId = groupName.GetSpanGroupId("spanTag");
    private readonly int SpanBodyId = groupName.GetSpanGroupId("spanBody");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        bool writeParagraph = !origin.HasFlag(MultiLineOrigin.PreserveHtml);
        if (writeParagraph) {
            writer.Write("<p>");
        }
        if (entireMatch.Groups[HtmlPreId].TryGetValue(out string? pre)) {
            _markdownParser.Value.ParseSingleline(pre, writer);
        }

        if (entireMatch.Groups[HtmlBodyId].TryGetValue(out string? htmlBody)) {
            // Span should be only special case allowed which allows for markdown parsing within it
            Match match = MarkdownRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (match.Groups[SpanTagId].TryGetValue(out string? spanTag) && match.Groups[SpanBodyId].TryGetValue(out string? spanBody)) {
                writer.Write(htmlSanitizer.Sanitize(spanTag).AsSpan()[..^7]);
                _markdownParser.Value.ParseMultiline(spanBody, writer, origin | MultiLineOrigin.Html);
                writer.Write("</span>");
            }
            else {
                writer.Write(htmlSanitizer.Sanitize(htmlBody));
            }
        }

        if (entireMatch.Groups[HtmlPostId].TryGetValue(out string? post)) {
            _markdownParser.Value.ParseSingleline(post, writer);
        }
        if (writeParagraph) {
            writer.Write("</p>");
        }
    }
}
