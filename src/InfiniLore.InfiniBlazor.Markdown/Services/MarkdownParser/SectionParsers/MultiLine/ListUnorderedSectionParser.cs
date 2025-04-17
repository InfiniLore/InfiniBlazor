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
[InjectableSingleton<IMultiLineSectionParser>("listUnordered")]
public class ListUnorderedSectionParser(IServiceProvider provider, ICachedRegexGroupNames groupName) : IMultiLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);

    private readonly int LTaskId = groupName.GetListGroupId("lTask");
    private readonly int LHeadId = groupName.GetListGroupId("lHead");
    private readonly int LBodyId = groupName.GetListGroupId("lBody");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        if (!group.TryGetValue(out string? listUnorderedBody)) return;

        List<Match> matchCollection = MarkdownRegexLib.ListItemBodyRegex.Matches(listUnorderedBody).ToList();
        int matchCount = matchCollection.Count;

        writer.Write("<ul>");
        for (int index = 0; index < matchCount; index++) {
            Match match = matchCollection[index];
            GroupCollection groups = match.Groups;

            writer.Write("<li>");
            if (groups[LTaskId].TryGetValue(out string? taskMarker)) {
                bool isChecked = taskMarker.ToLowerInvariant().Contains('x');
                writer.Write($"<input type=\"checkbox\" disabled {(isChecked ? "checked" : "")} /> ");
            }
            
            if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                _markdownParser.Value.ParseSingleline(listHeader, writer);
            }

            if (groups[LBodyId].TryGetValue(out string? listBody)) {
                string normalizedBody = NormalizationHelper.NormalizeIndentation(listBody);
                _markdownParser.Value.ParseMultiline(normalizedBody, writer,origin | MultiLineOrigin.PreserveHtml);
            }

            writer.Write("</li>");
        }

        writer.Write("</ul>");
    }
}
