// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.MarkdownParser;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Services.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("remainder")]
public class RemainderSectionParser(IServiceProvider provider) : IMultiLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match _, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        if (!group.TryGetValue(out string? paragraph)) return;
        if (paragraph.IsNullOrWhiteSpace()) return;
        bool writeParagraph = !origin.HasFlag(MultiLineOrigin.Html);

        if (writeParagraph) writer.Write("<p>");
        _markdownParser.Value.ParseSingleline(paragraph, writer);
        if (writeParagraph) writer.Write("</p>");
    }
}
