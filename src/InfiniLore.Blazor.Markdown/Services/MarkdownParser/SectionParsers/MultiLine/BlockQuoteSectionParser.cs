// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.Blazor.Markdown.Services.Pools;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("blockQuote")]
public class BlockQuoteSectionParser(IServiceProvider provider) : IMultiLineSectionParser {
    private readonly Lazy<IMarkdownParser> _markdownParser = new(provider.GetRequiredService<IMarkdownParser>);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match _, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        if (!group.TryGetValueSpan(out ReadOnlySpan<char> blockQuoteBody)) return;

        // Replace Regex usage with span-based logic:
        string normalized = NormalizeBlockQuote(ref blockQuoteBody);
        string adjustedBlockquote = NormalizationHelper.NormalizeIndentation(normalized);

        writer.Write("<blockquote>");
        _markdownParser.Value.ParseMultiline(adjustedBlockquote, writer, origin | MultiLineOrigin.PreserveHtml);
        writer.Write("</blockquote>");
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
