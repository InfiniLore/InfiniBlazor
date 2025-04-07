// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("codeBlock")]
public class CodeBlockSectionParser : IMultiLineSectionParser {
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer) {
        if (!entireMatch.Groups["cBody"].TryGetValue(out string? codeBlockBody)) return;
        string langName = entireMatch.Groups["cLang"].TryGetValue(out string? langNameValue)
            ? langNameValue
            : string.Empty;

        string langClass = langName.IsNotNullOrWhiteSpace()
            ? $" class=\"language-{langName}\""
            : string.Empty;


        writer.Write("<pre><code")
            .Write(langClass)
            .Write('>');
        ProcessCodeBlockContent(writer, codeBlockBody);
        writer.Write("</code></pre>");

    }

    private static void ProcessCodeBlockContent(IMarkdownWriter writer, string content) {
        ReadOnlySpan<char> remainder = content.AsSpan();
        int currentIndex = 0;

        foreach (ValueMatch match in HtmlSymbolLookup.CodeBlockRegex.EnumerateMatches(remainder)) {
            int matchIndex = match.Index;
            int matchLength = match.Length;
            if (currentIndex < matchIndex) {
                writer.Write(remainder.Slice(currentIndex, matchIndex - currentIndex));
            }

            if (HtmlSymbolLookup.CodeBlockLookup.TryGetValue(remainder.Slice(matchIndex, matchLength).ToString(), out string? replacement)) {
                writer.Write(replacement);
            }

            currentIndex = matchIndex + matchLength;
        }

        if (currentIndex < remainder.Length) {
            writer.Write(remainder[currentIndex..]);
        }
    }
}
