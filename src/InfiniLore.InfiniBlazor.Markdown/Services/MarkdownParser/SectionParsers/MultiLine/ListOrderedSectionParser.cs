// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("listOrdered")]
public class ListOrderedSectionParser : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private readonly int LTaskId = CachedRegexGroupNames.GetListGroupId("lTask");
    private readonly int LHeadId = CachedRegexGroupNames.GetListGroupId("lHead");
    private readonly int LBodyId = CachedRegexGroupNames.GetListGroupId("lBody");
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.TryGetValue(out string? listOrderedBody)) return;

        List<Match> matchCollection = MarkdownRegexLib.ListItemBodyRegex.Matches(listOrderedBody).ToList();
        int matchCount = matchCollection.Count;

        IMdNode olNode = currentNode.AddChildNode(MdElement.ListOrdered);
        for (int index = 0; index < matchCount; index++) {
            Match match = matchCollection[index];
            GroupCollection groups = match.Groups;
            
            IMdNode listItemNode = olNode.AddChildNode(MdElement.ListItem);
            
            if (groups[LBodyId].TryGetValue(out string? listBody) && listBody.IsNotNullOrWhiteSpace()) {
                string normalizedBody = NormalizationHelper.NormalizeIndentation(listBody);
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
}
