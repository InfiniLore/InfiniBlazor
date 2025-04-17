// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.MarkdownParser;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Services.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISingleLineSectionParser>("linkNested")]
public class LinkNestedSectionParser(IServiceProvider provider, ICachedRegexGroupNames groupNames) : ISingleLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);
    public SingleLineOrigin SkipOnOrigin => SingleLineOrigin.NotSkipped;
    
    private readonly int LnTextId = groupNames.GetSingleLineGroupId("lnText");
    private readonly int LnHrefId = groupNames.GetSingleLineGroupId("lnHref");
    private readonly int LnTitleId = groupNames.GetSingleLineGroupId("lnTitle");
    private readonly int LnBangId = groupNames.GetSingleLineGroupId("lnBang");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, SingleLineOrigin origin) {
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return;

        string titleText = entireMatch.Groups[LnTitleId].TryGetValue(out string? altTextValue) ? $" title=\"{altTextValue}\"" : string.Empty;

        if (entireMatch.Groups[LnBangId].Success) {
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

        _markdownParser.Value.ParseSingleline(linkText, writer, origin);
        writer.Write("</a>");
    }
}
