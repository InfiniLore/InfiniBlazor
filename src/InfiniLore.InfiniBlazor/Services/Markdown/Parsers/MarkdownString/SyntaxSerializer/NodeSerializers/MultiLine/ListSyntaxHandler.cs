// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.SyntaxSerializer.NodeSerializers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.List)]
public sealed class BaseListSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int LsId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListIdentifier);
    private static readonly int LTaskId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListTask);
    private static readonly int LHeadId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListHead);
    private static readonly int LBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.ListBody);
    
    public MarkdownStringMdSyntaxSerializerOrigin SkipOnOrigin => MarkdownStringMdSyntaxSerializerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownStringMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MarkdownStringMdSyntaxSerializerOrigin parentOrigin
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
            
                if (groups[LBodyId].TryGetValueSpan(out ReadOnlySpan<char> itemBody) && !itemBody.IsEmpty) {
                    string normalizedBody = LineNormalization.NormalizeLineIndentation(itemBody);
                    stack.PushMultiLineMatchesToStack(normalizedBody, listItemNode, parentOrigin | MarkdownStringMdSyntaxSerializerOrigin.PreserveHtml);
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
