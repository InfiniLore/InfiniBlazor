// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.List)]
public sealed class BaseListSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int LsId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListIdentifier);
    private static readonly int LTaskId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListTask);
    private static readonly int LHeadId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListHead);
    private static readonly int LBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListBody);
    private static readonly int LIndexId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListIndex);
    private static readonly int ListItemLeadingSpaces = MdRegexLib.GetGroupId(MdRegexGroupNames.ListItemLeadingSpaces);
    private static readonly int ListTaskItemLeadingSpaces = MdRegexLib.GetGroupId(MdRegexGroupNames.ListTaskItemLeadingSpaces);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch
    ) {
        if (!entireMatch.TryGetValue(out string? listBody)) return;
        bool isOrdered = !entireMatch.Groups[LsId].ValueSpan.Contains('-');

        MatchCollection matchCollection = MdRegexLib.ListItemBodyRegex.Matches(listBody);
        int matchCount = matchCollection.Count;
        Match[] matchArray = ArrayPool<Match>.Shared.Rent(matchCount);
        
        try {
            matchCollection.CopyTo(matchArray, 0);

            IMdSyntaxNode listNode = isOrdered
                ? ListOrderedMdSyntaxNode.Pool.Get()
                : ListUnOrderedMdSyntaxNode.Pool.Get();
            parentNode.AddChildNode(listNode);
            
            for (int i = 0; i < matchCount; i++) {
                GroupCollection groups = matchArray[i].Groups;

                ListItemMdSyntaxNode listItemNode = ListItemMdSyntaxNode.Pool.Get();
                listNode.AddChildNode(listItemNode);
                
                groups[ListItemLeadingSpaces].TryGetLength(out int listItemLeadingSpaces);
                listItemNode.WithLeadingSpaces(Math.Max(listItemLeadingSpaces, 0));
                
                groups[ListTaskItemLeadingSpaces].TryGetLength(out int listTaskItemLeadingSpaces);
                listItemNode.WithCheckLeadingSpaces(Math.Max(listTaskItemLeadingSpaces, 0));
                
                if (groups[LBodyId].TryGetValueSpan(out ReadOnlySpan<char> itemBody) && !itemBody.IsEmpty) {
                    string normalizedBody = LineNormalization.NormalizeLineIndentation(itemBody, out int leadingSpaces);
                    stack.PushMultiLineMatchesToStack(normalizedBody, listItemNode);
                    switch (listNode) {
                        case ListOrderedMdSyntaxNode ordered:
                            ordered.LeadingSpaces = leadingSpaces;
                            break;
                        case ListUnOrderedMdSyntaxNode unordered:
                            unordered.LeadingSpaces = leadingSpaces;
                            break;
                    }
                }

                if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                    stack.PushSingleLineMatchesToStack(listHeader, listItemNode);
                }
                
                if (groups[LIndexId].TryGetValue(out string? listIndex)) {
                    listItemNode.Index = listIndex;
                }

                // ReSharper disable once InvertIf
                if (groups[LTaskId].TryGetValue(out string? taskMarker)) {
                    listItemNode.IsCheckable = true;
                    listItemNode.OriginalCheckMarker = taskMarker;
                }
            }
        }
        finally {
            ArrayPool<Match>.Shared.Return(matchArray);
        }
    }
}
