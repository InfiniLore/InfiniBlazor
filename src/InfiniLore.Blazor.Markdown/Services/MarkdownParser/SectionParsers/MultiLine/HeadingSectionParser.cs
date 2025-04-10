// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("heading")]
public class HeadingSectionParser(IServiceProvider provider) : IMultiLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        if (!entireMatch.Groups["hLevel"].TryGetLength(out int headingLevel)) return;
        if (!entireMatch.Groups["hText"].TryGetValue(out string? headerText)) return;

        writer.Write("<h").Write(headingLevel).Write('>');
        _markdownParser.Value.ParseSingleline(headerText, writer);
        writer.Write("</h").Write(headingLevel).Write('>');
    }
}
