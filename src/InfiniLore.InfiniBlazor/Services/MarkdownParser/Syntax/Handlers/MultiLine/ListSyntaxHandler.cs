// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.List)]
public sealed class BaseListSyntaxHandler : IMdSyntaxHandler {
    private static readonly int LsId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListIdentifier);
    private static readonly int LTaskId = MdRegexLib.GetGroupId(MdRegexGroupNames.LTask);
    private static readonly int LHeadId = MdRegexLib.GetGroupId(MdRegexGroupNames.LHead);
    private static readonly int LBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.LBody);
    
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
    ) {
        if (!entireMatch.TryGetValue(out string? listBody)) return;
        bool isOrdered = !entireMatch.Groups[LsId].ValueSpan.Contains('-');

        MatchCollection matchCollection = MdRegexLib.ListItemBodyRegex.Matches(listBody);
        int matchCount = matchCollection.Count;
        Match[] matchArray = ArrayPool<Match>.Shared.Rent(matchCount);
        
        try {
            matchCollection.CopyTo(matchArray, 0);

            ListMdSyntaxNode listNode = ListMdSyntaxNode.Pool.Get();
            listNode.IsOrdered = isOrdered;
            parentNode.AddChildNode(listNode);
            
            for (int i = 0; i < matchCount; i++) {
                GroupCollection groups = matchArray[i].Groups;

                ListItemMdSyntaxNode listItemNode = ListItemMdSyntaxNode.Pool.Get();
                listNode.AddChildNode(listItemNode);
            
                if (groups[LBodyId].TryGetValueSpan(out ReadOnlySpan<char> itemBody) && !itemBody.IsEmpty) {
                    string normalizedBody = LineNormalization.NormalizeLineIndentation(itemBody);
                    stack.PushMultiLineMatchesToStack(normalizedBody, listItemNode, parentOrigin | MdSyntaxHandlerOrigin.PreserveHtml);
                }

                if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                    stack.PushSingleLineMatchesToStack(listHeader, listItemNode, parentOrigin);
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
