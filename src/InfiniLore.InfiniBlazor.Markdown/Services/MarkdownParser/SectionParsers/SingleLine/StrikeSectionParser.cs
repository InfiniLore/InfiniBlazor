// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Services.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISingleLineSectionParser>("strike")]
public class StrikeSectionParser(IServiceProvider provider, ICachedRegexGroupNames groupNames) : ISingleLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    public SingleLineOrigin SkipOnOrigin => SingleLineOrigin.Strike;
    
    private readonly int SId = groupNames.GetSingleLineGroupId("s");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, SingleLineOrigin origin) {
        if (!entireMatch.Groups[SId].TryGetValue(out string? italicValue)) return;

        writer.Write("<s>");
        _markdownParser.Value.ParseSingleline(italicValue, writer, origin | SkipOnOrigin);
        writer.Write("</s>");
    }
}
