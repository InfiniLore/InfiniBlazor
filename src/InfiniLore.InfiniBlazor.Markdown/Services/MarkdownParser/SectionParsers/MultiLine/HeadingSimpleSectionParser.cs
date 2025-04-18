// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("headingSimple")]
public class HeadingSimpleSectionParser(IServiceProvider provider, ICachedRegexGroupNames groupName) : IMultiLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    
    private readonly int HSTextId = groupName.GetMultiLineGroupId("hsText");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        if (!entireMatch.Groups[HSTextId].TryGetValue(out string? headerSimpleText)) return;

        writer.Write("<h1>");
        _markdownParser.Value.ParseSingleline(headerSimpleText, writer);
        writer.Write("</h1>");
    }
}
