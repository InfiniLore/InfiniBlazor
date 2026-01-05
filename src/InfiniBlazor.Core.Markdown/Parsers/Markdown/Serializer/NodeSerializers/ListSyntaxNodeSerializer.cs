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
public partial class ListSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex("""
        (?<list>
            ^[^\S\n]*(?<lsId>-(?!-)|\d+\.|\.\d+).+
            (?:\n(?:(?:-(?!-)|\d+\.|\.\d+)|(?:[\ ]+)).+)*
        )
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    
    [GeneratedRegex(@"^ *(?:-|(?<lIndex>\d*)\.)(?:(?<lTaskSpace> *)\[(?<lTask>[ xX~])])?(?:(?<lSpace> *)(?<lHead>.+)|(?<lHead> )|(?<lHead>))(?<lBody>(?:\n +.*)*)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex ListItemBodySyntax { get; }
    
    private static readonly int LsId = Syntax.GroupNumberFromName(MdRegexGroupNames.ListIdentifier);
    private static readonly int LTaskId = ListItemBodySyntax.GroupNumberFromName(MdRegexGroupNames.ListTask);
    private static readonly int LHeadId = ListItemBodySyntax.GroupNumberFromName(MdRegexGroupNames.ListHead);
    private static readonly int LBodyId = ListItemBodySyntax.GroupNumberFromName(MdRegexGroupNames.ListBody);
    private static readonly int LIndexId = ListItemBodySyntax.GroupNumberFromName(MdRegexGroupNames.ListIndex);
    private static readonly int ListItemLeadingSpaces = ListItemBodySyntax.GroupNumberFromName(MdRegexGroupNames.ListItemLeadingSpaces);
    private static readonly int ListTaskItemLeadingSpaces = ListItemBodySyntax.GroupNumberFromName(MdRegexGroupNames.ListTaskItemLeadingSpaces);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.TryGetValue(out string? listBody)) return;

        bool isOrdered = !match.Groups[LsId].ValueSpan.Contains('-');

        MatchCollection matchCollection = ListItemBodySyntax.Matches(listBody);
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
