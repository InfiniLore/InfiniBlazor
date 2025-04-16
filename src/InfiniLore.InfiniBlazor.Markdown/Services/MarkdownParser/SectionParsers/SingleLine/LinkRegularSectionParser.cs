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
[InjectableSingleton<ISingleLineSectionParser>("linkRegular")]
public class LinkRegularSectionParser(IServiceProvider provider, ICachedRegexGroupNames groupNames) : ISingleLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    public SingleLineOrigin SkipOnOrigin => SingleLineOrigin.Link;
    
    private readonly int LrTextId = groupNames.GetSingleLineGroupId("lrText");
    private readonly int LrHrefId = groupNames.GetSingleLineGroupId("lrHref");
    private readonly int LrTitleId = groupNames.GetSingleLineGroupId("lrTitle");
    private readonly int LrBangId = groupNames.GetSingleLineGroupId("lrBang");
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, SingleLineOrigin origin) {
        if (!entireMatch.Groups[LrTextId].TryGetValue(out string? linkText)) return;
        if (!entireMatch.Groups[LrHrefId].TryGetValue(out string? linkHref)) return;

        string titleText = entireMatch.Groups[LrTitleId].TryGetValue(out string? altTextValue) ? $" title=\"{altTextValue}\"" : string.Empty;

        if (entireMatch.Groups[LrBangId].Success) {
            writer.Write("<img src=\"");
            writer.Write(linkHref);
            writer.Write("\" alt=\"");
            writer.Write(linkText);
            writer.Write('"');
            writer.Write(titleText);
            writer.Write('>');
            return;
        }

        writer.Write("<a href=\"");
        writer.Write(linkHref);
        writer.Write("\">");
        _markdownParser.Value.ParseSingleline(linkText, writer, origin | SkipOnOrigin);
        writer.Write("</a>");
    }
}
