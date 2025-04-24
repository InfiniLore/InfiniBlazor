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
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!group.TryGetValueSpan(out ReadOnlySpan<char> blockQuoteBody)) return;

        // Replace Regex usage with span-based logic:
        ReadOnlySpan<char> normalized = NormalizeBlockQuote(blockQuoteBody);
        string adjustedBlockquote = LineNormalizationHelper.NormalizeLineIndentation(normalized);

        IMdNode blockquoteNode = currentNode.AddChildNode(MdElement.Blockquote);
        engine.AddMultiLineMatchesToStack(adjustedBlockquote, blockquoteNode, origin | HandlerOrigin.PreserveHtml);
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
