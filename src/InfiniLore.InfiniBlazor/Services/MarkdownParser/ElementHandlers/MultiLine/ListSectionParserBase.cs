// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class ListHandlerBase(ILineNormalizationService lineNormalizationHelper) : IMarkdownElementHandler {
    private static readonly int LTaskId = MarkdownRegexLib.GetListGroupId("lTask");
    private static readonly int LHeadId = MarkdownRegexLib.GetListGroupId("lHead");
    private static readonly int LBodyId = MarkdownRegexLib.GetListGroupId("lBody");
    
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;
    
    protected abstract MarkdownElement ListType { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMarkdownSyntaxNode currentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        if (!entireMatch.TryGetValue(out string? listBody)) return;

        MatchCollection matchCollection = MarkdownRegexLib.ListItemBodyRegex.Matches(listBody);
        int matchCount = matchCollection.Count;
        Match[] matchArray = ArrayPool<Match>.Shared.Rent(matchCount);
        
        try {
            matchCollection.CopyTo(matchArray, 0);

            IMarkdownSyntaxNode listNode = currentNode.AddChildNode(ListType);
            for (int i = 0; i < matchCount; i++) {
                GroupCollection groups = matchArray[i].Groups;

                IMarkdownSyntaxNode listItemNode = listNode.AddChildNode(MarkdownElement.ListItem);
            
                if (groups[LBodyId].TryGetValueSpan(out ReadOnlySpan<char> itemBody) && !itemBody.IsEmpty) {
                    string normalizedBody = lineNormalizationHelper.NormalizeLineIndentation(itemBody);
                    engine.AddMultiLineMatchesToStack(normalizedBody, listItemNode, origin | HandlerOrigin.PreserveHtml);
                }

                if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                    engine.AddSingleLineMatchesToStack(listHeader, listItemNode, origin);
                }

                // ReSharper disable once InvertIf
                if (groups[LTaskId].TryGetValue(out string? taskMarker)) {
                    bool isChecked = taskMarker.ToLowerInvariant().Contains('x');
                    MarkdownElement element = isChecked ? MarkdownElement.CheckboxSelected : MarkdownElement.CheckboxUnselected;
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
public class ListOrderedHandlerBase(ILineNormalizationService lineNormalizationHelper) : ListHandlerBase(lineNormalizationHelper) {
    protected override MarkdownElement ListType => MarkdownElement.ListOrdered;
}

[InjectableSingleton<IMarkdownElementHandler>("listUnordered")]
public class ListUnorderedHandler(ILineNormalizationService lineNormalizationHelper) : ListHandlerBase(lineNormalizationHelper) {
    protected override MarkdownElement ListType => MarkdownElement.ListUnordered;
}
