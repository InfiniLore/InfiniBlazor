// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class ListHandlerBase : IMarkdownElementHandler {
    private static readonly int LTaskId = MarkdownRegexLib.GetListGroupId("lTask");
    private static readonly int LHeadId = MarkdownRegexLib.GetListGroupId("lHead");
    private static readonly int LBodyId = MarkdownRegexLib.GetListGroupId("lBody");
    
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;
    
    protected abstract MdElement ListType { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.TryGetValue(out string? listBody)) return;

        MatchCollection matchCollection = MarkdownRegexLib.ListItemBodyRegex.Matches(listBody);
        int matchCount = matchCollection.Count;
        Match[] matchArray = ArrayPool<Match>.Shared.Rent(matchCount);
        
        try {
            matchCollection.CopyTo(matchArray, 0);

            IMdNode listNode = currentNode.AddChildNode(ListType);
            for (int i = 0; i < matchCount; i++) {
                GroupCollection groups = matchArray[i].Groups;

                IMdNode listItemNode = listNode.AddChildNode(MdElement.ListItem);
            
                if (groups[LBodyId].TryGetValueSpan(out ReadOnlySpan<char> itemBody) && !itemBody.IsEmpty) {
                    string normalizedBody = LineNormalizationHelper.NormalizeLineIndentation(itemBody);
                    engine.AddMultiLineMatchesToStack(normalizedBody, listItemNode, origin | HandlerOrigin.PreserveHtml);
                }

                if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                    engine.AddSingleLineMatchesToStack(listHeader, listItemNode, origin);
                }

                // ReSharper disable once InvertIf
                if (groups[LTaskId].TryGetValue(out string? taskMarker)) {
                    bool isChecked = taskMarker.ToLowerInvariant().Contains('x');
                    MdElement element = isChecked ? MdElement.CheckboxSelected : MdElement.CheckboxUnselected;
                    engine.PushElementToStack(null, listItemNode, origin, element);
                }
            }
        }
        finally {
            ArrayPool<Match>.Shared.Return(matchArray);
        }
    }
}

[InjectableSingleton<IMarkdownElementHandler>("listOrdered")]
public class ListOrderedHandlerBase : ListHandlerBase {
    protected override MdElement ListType => MdElement.ListOrdered;
}

[InjectableSingleton<IMarkdownElementHandler>("listUnordered")]
public class ListUnorderedHandler : ListHandlerBase {
    protected override MdElement ListType => MdElement.ListUnordered;
}
