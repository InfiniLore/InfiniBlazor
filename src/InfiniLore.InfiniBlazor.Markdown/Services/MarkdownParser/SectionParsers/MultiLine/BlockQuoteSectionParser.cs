// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("blockQuote")]
public class BlockQuoteSectionParser : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!group.TryGetValueSpan(out ReadOnlySpan<char> blockQuoteBody)) return;

        // Replace Regex usage with span-based logic:
        ReadOnlySpan<char> normalized = NormalizeBlockQuote(blockQuoteBody);
        string adjustedBlockquote = NormalizationHelper.NormalizeIndentation(normalized);

        IMdNode blockquoteNode = currentNode.AddChildNode(MdElement.Blockquote);
        parser.AddMultiLineMatchesToStack(adjustedBlockquote, blockquoteNode, origin | ParserOrigin.PreserveHtml);
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
