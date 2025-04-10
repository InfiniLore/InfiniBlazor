// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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
        if (!group.TryGetValue(out string? blockQuoteBody)) return;

        string normalized = MarkdownRegexLib.NormalizeBlockQuoteRegex.Replace(blockQuoteBody, string.Empty);
        string adjustedBlockquote = NormalizationHelper.NormalizeIndentation(normalized);

        writer.Write("<blockquote>");
        _markdownParser.Value.ParseMultiline(adjustedBlockquote, writer, origin | MultiLineOrigin.PreserveHtml);
        writer.Write("</blockquote>");
    }
}
