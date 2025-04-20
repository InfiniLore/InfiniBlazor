// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Pools;
using System.Text;
using System.Text.RegularExpressions;
using CodeOfChaos.Extensions;

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

        IMdNode blockquoteNode = currentNode.AddChildNode(MdElement.Blockquote);
        parser.AddMultiLineMatchesToStack(adjustedBlockquote, blockquoteNode, origin | ParserOrigin.PreserveHtml);
    }

    private static string NormalizeBlockQuote(ref ReadOnlySpan<char> span) {
        // Use a ValueStringBuilder for efficient memory writing
        StringBuilder builder = StringBuilderPool.Get();
        try {
            bool isFirstLine = true;
            foreach (ReadOnlySpan<char> line in span.EnumerateLines()) {
                if (!isFirstLine) builder.Append('\n');

                ReadOnlySpan<char> trimmedLine = line.TrimStart();
                if (trimmedLine.IsEmpty) continue;

                if (trimmedLine[0] == '>') {
                    int contentStart = 1;
                    while (contentStart < trimmedLine.Length && trimmedLine[contentStart].IsWhiteSpace()) {
                        contentStart++;
                    }

                    builder.Append(trimmedLine[contentStart..]);
                    isFirstLine = false;
                    continue;
                }

                builder.Append(trimmedLine);
                isFirstLine = false;
            }

            return builder.ToString();
        }
        finally {
            StringBuilderPool.Return(builder);
        }

    }

}
