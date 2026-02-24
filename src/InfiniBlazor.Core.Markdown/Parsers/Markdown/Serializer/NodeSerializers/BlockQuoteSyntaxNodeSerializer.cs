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
public sealed partial class BlockQuoteSyntaxNodeSerializer : BaseMdSyntaxNodeSerializer {
    [GeneratedRegex(@"\G^>[\ ]*(?:.+(?:\n>[^\n]*)*)$", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex RegexRule { get; }
    protected override Regex Syntax { get; } = RegexRule;

    private static readonly char[] STriggerCharacters = ['>'];
    public override char[] TriggerCharacters => STriggerCharacters;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override void Serialize(
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
