// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class BlockQuoteSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int BlockQuoteId = MdRegexLib.GetGroupId(MdRegexGroupNames.BlockQuote);

    public Regex Syntax { get; } = MdRegexLib.BlockQuoteRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        Group group = match.Groups[BlockQuoteId];
        if (!group.TryGetValueSpan(out ReadOnlySpan<char> blockQuoteBody)) return;

        string adjustedBlockquote = LineNormalization.NormalizeBlockQuote(blockQuoteBody, out int leadingSpaces);

        BlockQuoteMdSyntaxNode blockQuoteNode = BlockQuoteMdSyntaxNode.Pool.Get();
        blockQuoteNode.WithLeadingSpaces(leadingSpaces);

        parentNode.AddChildNode(blockQuoteNode);
        stack.PushMultiLineMatchesToStack(adjustedBlockquote, blockQuoteNode);
    }
}
