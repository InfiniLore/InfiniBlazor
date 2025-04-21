// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("listUnordered")]
public class ListUnorderedSectionParser : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;

    private static readonly int LTaskId = CachedRegexGroupNames.GetListGroupId("lTask");
    private static readonly int LHeadId = CachedRegexGroupNames.GetListGroupId("lHead");
    private static readonly int LBodyId = CachedRegexGroupNames.GetListGroupId("lBody");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.TryGetValue(out string? listUnorderedBody)) return;

        List<Match> matchCollection = MarkdownRegexLib.ListItemBodyRegex.Matches(listUnorderedBody).ToList();
        int matchCount = matchCollection.Count;
        
        IMdNode ulNode = currentNode.AddChildNode(MdElement.ListUnordered);
        for (int index = 0; index < matchCount; index++) {
            Match match = matchCollection[index];
            GroupCollection groups = match.Groups;

            IMdNode listItemNode = ulNode.AddChildNode(MdElement.ListItem);
            if (groups[LTaskId].TryGetValue(out string? taskMarker)) {
                bool isChecked = taskMarker.Contains('x');
                IMdNode inputNode = listItemNode.AddChildNode(MdElement.Input);
                inputNode.WithAttribute("type", "checkbox");
                inputNode.WithAttribute("disabled", isChecked ? "checked" : "");
            }

            if (groups[LBodyId].TryGetValue(out string? listBody)) {
                string normalizedBody = NormalizationHelper.NormalizeIndentation(listBody);
                parser.AddMultiLineMatchesToStack(normalizedBody, listItemNode, origin | ParserOrigin.PreserveHtml);
            }
            
            // ReSharper disable once InvertIf
            if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                parser.AddSingleLineMatchesToStack(listHeader, listItemNode, origin);
            }
        }
    }
}
