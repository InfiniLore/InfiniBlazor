// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
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
    protected abstract bool IsOrdered { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMdSyntaxNode parentNode,
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

            ListMdSyntaxNode listNode = ListMdSyntaxNode.Shared.Get();
            listNode.IsOrdered = IsOrdered;
            parentNode.AddChildNode(listNode);
            
            for (int i = 0; i < matchCount; i++) {
                GroupCollection groups = matchArray[i].Groups;

                ListItemMdSyntaxNode listItemNode = ListItemMdSyntaxNode.Shared.Get();
                listNode.AddChildNode(listItemNode);
            
                if (groups[LBodyId].TryGetValueSpan(out ReadOnlySpan<char> itemBody) && !itemBody.IsEmpty) {
                    string normalizedBody = lineNormalizationHelper.NormalizeLineIndentation(itemBody);
                    engine.PushMultiLineMatchesToStack(normalizedBody, listItemNode, origin | HandlerOrigin.PreserveHtml);
                }

                if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                    engine.PushSingleLineMatchesToStack(listHeader, listItemNode, origin);
                }

                // ReSharper disable once InvertIf
                if (groups[LTaskId].TryGetValue(out string? taskMarker)) {
                    listItemNode.IsCheckable = true;
                    listItemNode.IsChecked = taskMarker.ToLowerInvariant().Contains('x');
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
    protected override bool IsOrdered => true;
}

[InjectableSingleton<IMarkdownElementHandler>("listUnordered")]
public class ListUnorderedHandler(ILineNormalizationService lineNormalizationHelper) : ListHandlerBase(lineNormalizationHelper) {
    protected override MarkdownElement ListType => MarkdownElement.ListUnordered;
    protected override bool IsOrdered => false;
}
