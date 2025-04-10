// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Ganss.Xss;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("htmlBody")]
public class HtmlBodySectionParser(IServiceProvider provider, IHtmlSanitizer htmlSanitizer) : IMultiLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        bool writeParagraph = !origin.HasFlag(MultiLineOrigin.PreserveHtml);
        if (writeParagraph) {
            writer.Write("<p>");
        }
        if (entireMatch.Groups["htmlPre"].TryGetValue(out string? pre)) {
            _markdownParser.Value.ParseSingleline(pre, writer);
        }

        if (entireMatch.Groups["htmlBody"].TryGetValue(out string? htmlBody)) {
            var match = MarkdownRegexLib.FindSpanHtmlRegex.Match(htmlBody);
            if (match.Groups["spanTag"].TryGetValue(out string? spanTag) && match.Groups["spanBody"].TryGetValue(out string? spanBody)) {
                writer.Write(htmlSanitizer.Sanitize(spanTag).AsSpan()[..^7]);
                _markdownParser.Value.ParseMultiline(spanBody, writer, origin | MultiLineOrigin.Html);
                writer.Write("</span>");
            }
            else {
                writer.Write(htmlSanitizer.Sanitize(htmlBody));
            }
        }

        if (entireMatch.Groups["htmlPost"].TryGetValue(out string? post)) {
            _markdownParser.Value.ParseSingleline(post, writer);
        }
        if (writeParagraph) {
            writer.Write("</p>");
        }
    }
}
