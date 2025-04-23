// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("listUnordered")]
public class ListUnorderedSectionParser : ISectionHandler {

    private static readonly int LTaskId = CachedRegexGroupNames.GetListGroupId("lTask");
    private static readonly int LHeadId = CachedRegexGroupNames.GetListGroupId("lHead");
    private static readonly int LBodyId = CachedRegexGroupNames.GetListGroupId("lBody");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.TryGetValue(out string? listUnorderedBody)) return;

        MatchCollection matchCollection = MarkdownRegexLib.ListItemBodyRegex.Matches(listUnorderedBody);
        int matchCount = matchCollection.Count;
        GroupCollection[] groupCollections = ArrayPool<GroupCollection>.Shared.Rent(matchCount);
        try {
            for (int i = matchCount - 1; i >= 0; i--) {
                Match match = matchCollection[i];
                groupCollections[i] = match.Groups;
            }

            IMdNode ulNode = currentNode.AddChildNode(MdElement.ListUnordered);
            for (int i = 0; i < matchCount; i++) {
                GroupCollection groups = groupCollections[i];

                IMdNode listItemNode = ulNode.AddChildNode(MdElement.ListItem);

                if (groups[LBodyId].TryGetValueSpan(out ReadOnlySpan<char> listBody) && !listBody.IsEmpty) {
                    string normalizedBody = NormalizationHelper.NormalizeIndentation(ref listBody);
                    parser.AddMultiLineMatchesToStack(normalizedBody, listItemNode, origin | ParserOrigin.PreserveHtml);
                }

                if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                    parser.AddSingleLineMatchesToStack(listHeader, listItemNode, origin);
                }

                // ReSharper disable once InvertIf
                if (groups[LTaskId].TryGetValue(out string? taskMarker)) {
                    bool isChecked = taskMarker.ToLowerInvariant().Contains('x');
                    MdElement element = isChecked ? MdElement.CheckboxSelected : MdElement.CheckboxUnselected;
                    parser.PushElementToStack(null, listItemNode, origin, element);
                }
            }
        }

        finally {
            ArrayPool<GroupCollection>.Shared.Return(groupCollections);
        }
        
    }
}
