// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("blockQuote")]
public class BlockQuoteHandler(ILineNormalizationService lineNormalizationHelper, IPoolCache poolCache) : IMarkdownElementHandler {
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        if (!group.TryGetValueSpan(out ReadOnlySpan<char> blockQuoteBody)) return;

        // Replace Regex usage with span-based logic:
        ReadOnlySpan<char> normalized = NormalizeBlockQuote(blockQuoteBody);
        string adjustedBlockquote = lineNormalizationHelper.NormalizeLineIndentation(normalized);

        BlockQuoteMdSyntaxNode blockQuoteNode = BlockQuoteMdSyntaxNode.Shared.Get();
        parentNode.AddChildNode(blockQuoteNode);
        engine.PushMultiLineMatchesToStack(adjustedBlockquote, blockQuoteNode, origin | HandlerOrigin.PreserveHtml);
    }

    private ReadOnlySpan<char> NormalizeBlockQuote(ReadOnlySpan<char> span) {
        // Use a ValueStringBuilder for efficient memory writing
        StringBuilder builder = poolCache.StringBuilderPool.Get();
        try {
            foreach (ReadOnlySpan<char> line in span.EnumerateLines()) {
                // Example: Remove leading '>' and any extra whitespace
                ReadOnlySpan<char> trimmedLine = line.TrimStart();

                if (trimmedLine.StartsWith('>')) {
                    trimmedLine = trimmedLine[1..];// Remove '>'
                }

                // Append the normalized line back to the builder
                builder.Append(trimmedLine);
                builder.Append('\n');
            }
            if (builder.Length > 0) builder.Length--; // Remove the last newline
            return builder.ToString();
        }
        finally {
            poolCache.StringBuilderPool.Return(builder);
        }

    }
}
