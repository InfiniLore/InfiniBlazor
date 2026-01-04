// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ListSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int LsId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListIdentifier);
    private static readonly int LTaskId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListTask);
    private static readonly int LHeadId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListHead);
    private static readonly int LBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListBody);
    private static readonly int LIndexId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListIndex);
    private static readonly int ListItemLeadingSpaces = MdRegexLib.GetGroupId(MdRegexGroupNames.ListItemLeadingSpaces);
    private static readonly int ListTaskItemLeadingSpaces = MdRegexLib.GetGroupId(MdRegexGroupNames.ListTaskItemLeadingSpaces);

    public Regex Syntax { get; } = MdRegexLib.ListRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.TryGetValue(out string? listBody)) return;

        bool isOrdered = !match.Groups[LsId].ValueSpan.Contains('-');

        MatchCollection matchCollection = MdRegexLib.ListItemBodyRegex.Matches(listBody);
        int matchCount = matchCollection.Count;
        Match[] matchArray = ArrayPool<Match>.Shared.Rent(matchCount);

        try {
            matchCollection.CopyTo(matchArray, 0);

            IMdSyntaxNode listNode = isOrdered
                ? MdSyntaxNodePool<ListOrderedMdSyntaxNode>.Shared.Get()
                : MdSyntaxNodePool<ListUnOrderedMdSyntaxNode>.Shared.Get();
            parentNode.AddChildNode(listNode);

            for (int i = 0; i < matchCount; i++) {
                GroupCollection groups = matchArray[i].Groups;

                ListItemMdSyntaxNode listItemNode = MdSyntaxNodePool<ListItemMdSyntaxNode>.Shared.Get();
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
                            ordered.WithLeadingSpaces(leadingSpaces);
                            break;
                        case ListUnOrderedMdSyntaxNode unordered:
                            unordered.WithLeadingSpaces(leadingSpaces);
                            break;
                    }
                }

                if (groups[LHeadId].TryGetValue(out string? listHeader)) {
                    stack.PushSingleLineMatchesToStack(listHeader, listItemNode);
                }

                if (groups[LIndexId].TryGetValue(out string? listIndex)) {
                    listItemNode.WithIndex(listIndex);
                }

                // ReSharper disable once InvertIf
                if (groups[LTaskId].TryGetValue(out string? taskMarker)) {
                    listItemNode.WithCheckMarker(taskMarker);
                }
            }
        }
        finally {
            ArrayPool<Match>.Shared.Return(matchArray);
        }
    }
}
