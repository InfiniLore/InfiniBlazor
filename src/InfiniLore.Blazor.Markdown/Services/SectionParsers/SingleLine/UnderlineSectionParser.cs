// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISingleLineSectionParser>("underline")]
public class UnderlineSectionParser(IServiceProvider provider) : ISingleLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    public SingleLineOrigin SkipOnOrigin => SingleLineOrigin.Underline;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, SingleLineOrigin origin) {
        if (!entireMatch.Groups["u"].TryGetValue(out string? underlineValue)) return;

        writer.Write("<span style=\"text-decoration: underline;\">");
        _markdownParser.Value.ParseSingleline(underlineValue, writer, origin | SkipOnOrigin);
        writer.Write("</span>");
    }
}
