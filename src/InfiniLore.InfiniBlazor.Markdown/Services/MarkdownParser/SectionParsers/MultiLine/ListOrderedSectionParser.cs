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
[InjectableSingleton<ISectionHandler>("listOrdered")]
public class ListOrderedSectionParser : ISectionHandler {
    private readonly int LBodyId = CachedRegexGroupNames.GetListGroupId("lBody");
    private readonly int LHeadId = CachedRegexGroupNames.GetListGroupId("lHead");

    private readonly int LTaskId = CachedRegexGroupNames.GetListGroupId("lTask");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.TryGetValue(out string? listOrderedBody)) return;

        MatchCollection matchCollection = MarkdownRegexLib.ListItemBodyRegex.Matches(listOrderedBody);
        int matchCount = matchCollection.Count;
        Match[] matchArray = ArrayPool<Match>.Shared.Rent(matchCount);
        
        try {
            matchCollection.CopyTo(matchArray, 0);

            IMdNode olNode = currentNode.AddChildNode(MdElement.ListOrdered);
            for (int i = 0; i < matchCount; i++) {
                GroupCollection groups = matchArray[i].Groups;

                IMdNode listItemNode = olNode.AddChildNode(MdElement.ListItem);
            
                if (groups[LBodyId].TryGetValueSpan(out ReadOnlySpan<char> listBody) && !listBody.IsEmpty) {
                    string normalizedBody = NormalizationHelper.NormalizeIndentation(ref listBody);
                    parser.AddMultiLineMatchesToStack(normalizedBody, listItemNode, origin | ParserOrigin.PreserveHtml);
                }

                if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                    parser.AddSingleLineMatchesToStack(listHeader, listItemNode, origin);
                }

                // ReSharper disable once InvertIf
                if (groups[LTaskId].TryGetValue(out string? taskMarker)) {
                    bool isChecked = taskMarker.Contains('x');
                    MdElement element = isChecked ? MdElement.CheckboxSelected : MdElement.CheckboxUnselected;
                    parser.PushElementToStack(null, listItemNode, origin, element);
                }
            }
        }

        finally {
            ArrayPool<Match>.Shared.Return(matchArray);
        }
        
    }
}
