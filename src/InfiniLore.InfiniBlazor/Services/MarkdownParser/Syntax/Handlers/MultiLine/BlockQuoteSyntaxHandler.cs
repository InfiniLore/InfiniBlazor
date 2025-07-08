// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.BlockQuote)]
public sealed class BlockQuoteSyntaxHandler : IMdSyntaxHandler {
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;
    private static readonly int BlockQuoteId = MdRegexLib.GetGroupId(MdRegexGroupNames.BlockQuote);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
    ) {
        Group group = entireMatch.Groups[BlockQuoteId];
        if (!group.TryGetValueSpan(out ReadOnlySpan<char> blockQuoteBody)) return;

        // Replace Regex usage with span-based logic:
        string adjustedBlockquote = LineNormalization.NormalizeBlockQuote(blockQuoteBody);

        BlockQuoteMdSyntaxNode blockQuoteNode = BlockQuoteMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(blockQuoteNode);
        stack.PushMultiLineMatchesToStack(adjustedBlockquote, blockQuoteNode, parentOrigin | MdSyntaxHandlerOrigin.PreserveHtml);
    }
}
