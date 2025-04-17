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
[InjectableSingleton<IMultiLineSectionParser>("heading")]
public class HeadingSectionParser(IServiceProvider provider, ICachedRegexGroupNames groupName) : IMultiLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    
    private readonly int HLevelId = groupName.GetMultiLineGroupId("hLevel");
    private readonly int HTextId = groupName.GetMultiLineGroupId("hText");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        if (!entireMatch.Groups[HLevelId].TryGetLength(out int headingLevel)) return;
        if (!entireMatch.Groups[HTextId].TryGetValue(out string? headerText)) return;

        writer.Write("<h").Write(headingLevel).Write('>');
        _markdownParser.Value.ParseSingleline(headerText, writer);
        writer.Write("</h").Write(headingLevel).Write('>');
    }
}
