// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Pools;
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
    public void HandleMatch(Match entireMatch, Group group, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!group.TryGetValueSpan(out ReadOnlySpan<char> blockQuoteBody)) return;

        // Replace Regex usage with span-based logic:
        string normalized = NormalizeBlockQuote(ref blockQuoteBody);
        string adjustedBlockquote = NormalizationHelper.NormalizeIndentation(normalized);

        IMdNode blockquoteNode = currentNode.AddChild(MdElement.Blockquote);
        parser.AddMultiLineMatchesToStack(adjustedBlockquote, blockquoteNode, origin | ParserOrigin.PreserveHtml);
    }

    private static string NormalizeBlockQuote(ref ReadOnlySpan<char> span) {
        // Use a ValueStringBuilder for efficient memory writing
        StringBuilder builder = StringBuilderPool.Get();
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

            return builder.ToString();
        }
        finally {
            StringBuilderPool.Return(builder);
        }
    }

}
