// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("blockQuote")]
public class BlockQuoteHandler : IMarkdownElementHandler {
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin, CancellationToken ct = default) {
        if (!group.TryGetValueSpan(out ReadOnlySpan<char> blockQuoteBody)) return ValueTask.CompletedTask;

        // Replace Regex usage with span-based logic:
        ReadOnlySpan<char> normalized = NormalizeBlockQuote(blockQuoteBody);
        string adjustedBlockquote = LineNormalizationHelper.NormalizeLineIndentation(normalized);

        IMarkdownSyntaxNode blockquoteNode = currentNode.AddChildNode(MarkdownElement.Blockquote);
        engine.AddMultiLineMatchesToStack(adjustedBlockquote, blockquoteNode, origin | HandlerOrigin.PreserveHtml);
        return ValueTask.CompletedTask;
    }

    private static ReadOnlySpan<char> NormalizeBlockQuote(ReadOnlySpan<char> span) {
        // Use a ValueStringBuilder for efficient memory writing
        StringBuilder builder = PoolCache.StringBuilderPool.Get();
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
            PoolCache.StringBuilderPool.Return(builder);
        }

    }
}
