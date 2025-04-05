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
[InjectableSingleton<ISingleLineSectionParser>("supScript")]
public class SuperScriptSectionParser(IServiceProvider provider) : ISingleLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    public SingleLineOrigin SkipOnOrigin => SingleLineOrigin.SuperScript;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, SingleLineOrigin origin) {
        if (!entireMatch.Groups["sp"].TryGetValue(out string? boldValue)) return;

        writer.Write("<sup>");
        _markdownParser.Value.ParseSingleline(boldValue, writer, origin | SkipOnOrigin);
        writer.Write("</sup>");
    }
}
