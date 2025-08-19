// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer.NodeSerializers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.BlockQuote)]
public sealed class BlockQuoteSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.NotSkipped;
    private static readonly int BlockQuoteId = MdRegexLib.GetGroupId(MdRegexGroupNames.BlockQuote);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
    ) {
        Group group = entireMatch.Groups[BlockQuoteId];
        if (!group.TryGetValueSpan(out ReadOnlySpan<char> blockQuoteBody)) return;

        // Replace Regex usage with span-based logic:
        string adjustedBlockquote = LineNormalization.NormalizeBlockQuote(blockQuoteBody);

        BlockQuoteMdSyntaxNode blockQuoteNode = BlockQuoteMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(blockQuoteNode);
        stack.PushMultiLineMatchesToStack(adjustedBlockquote, blockQuoteNode, parentOrigin | MdSyntaxSerializerOrigin.PreserveHtml);
    }
}
