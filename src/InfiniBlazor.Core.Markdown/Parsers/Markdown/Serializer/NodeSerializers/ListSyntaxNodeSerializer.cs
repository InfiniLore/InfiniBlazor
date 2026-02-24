// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class ListSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex("""
        \G
        ^[^\S\n]*(?<id>-(?!-)|\d+\.|\.\d+).+
        (?:\n(?:(?:-(?!-)|\d+\.|\.\d+)|(?:[\ ]+)).+)*
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    [GeneratedRegex(@"^ *(?:-|(?<index>\d*)\.)(?:(?<taskSpace> *)\[(?<task>[ xX~])])?(?:(?<space> *)(?<head>.+)|(?<head> )|(?<head>))(?<body>(?:\n +.*)*)", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex ListItemBodyRegexRule { get; }

    public override ReadOnlySpan<char> TriggerCharacters => ['-', ' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    private static readonly int LsId = RegexRule.GroupNumberFromName("id");
    private static readonly int LIndexId = ListItemBodyRegexRule.GroupNumberFromName("index");
    private static readonly int ListTaskItemLeadingSpaces = ListItemBodyRegexRule.GroupNumberFromName("taskSpace");
    private static readonly int LTaskId = ListItemBodyRegexRule.GroupNumberFromName("task");
    private static readonly int ListItemLeadingSpaces = ListItemBodyRegexRule.GroupNumberFromName("space");
    private static readonly int LHeadId = ListItemBodyRegexRule.GroupNumberFromName("head");
    private static readonly int LBodyId = ListItemBodyRegexRule.GroupNumberFromName("body");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        string listBody = match.Value;
        bool isOrdered = !match.Groups[LsId].ValueSpan.Contains('-');

        MatchCollection matchCollection = ListItemBodyRegexRule.Matches(listBody);
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
