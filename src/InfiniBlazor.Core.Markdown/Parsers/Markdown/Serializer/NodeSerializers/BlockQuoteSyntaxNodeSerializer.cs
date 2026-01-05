// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class BlockQuoteSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"^>[\ ]*(?:.+(?:\n>[^\n]*)*)$", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
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
        ReadOnlySpan<char> blockQuoteBody = match.ValueSpan;
        string adjustedBlockquote = LineNormalization.NormalizeBlockQuote(blockQuoteBody, out int leadingSpaces);

        BlockQuoteMdSyntaxNode blockQuoteNode = MdSyntaxNodePool<BlockQuoteMdSyntaxNode>.Shared.Get();
        blockQuoteNode.WithLeadingSpaces(leadingSpaces);

        parentNode.AddChildNode(blockQuoteNode);
        stack.PushMultiLineMatchesToStack(adjustedBlockquote, blockQuoteNode);
    }
}
