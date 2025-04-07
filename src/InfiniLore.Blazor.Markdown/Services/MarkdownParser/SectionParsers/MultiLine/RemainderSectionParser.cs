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
[InjectableSingleton<IMultiLineSectionParser>("remainder")]
public class RemainderSectionParser(IServiceProvider provider) : IMultiLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match _, Group group, IMarkdownWriter writer) {
        if (!group.TryGetValue(out string? paragraph)) return;
        if (paragraph.IsNullOrWhiteSpace()) return;

        writer.Write("<p>");
        _markdownParser.Value.ParseSingleline(paragraph, writer);
        writer.Write("</p>");
    }
}
