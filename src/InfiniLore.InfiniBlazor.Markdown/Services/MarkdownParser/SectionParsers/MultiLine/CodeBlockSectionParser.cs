// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMultiLineSectionParser>("codeBlock")]
public class CodeBlockSectionParser(ICachedRegexGroupNames groupNames) : IMultiLineSectionParser {
    private const string CodeBlockStartMarker = "<pre><code";
    private const string CodeBlockLang = " class=\"language-";
    private const string CodeBlockEndMarker = "</code></pre>";
    
    private readonly int CBodyId = groupNames.GetMultiLineGroupId("cBody");
    private readonly int CLangId = groupNames.GetMultiLineGroupId("cLang");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, MultiLineOrigin origin) {
        if (!entireMatch.Groups[CBodyId].TryGetValueSpan(out ReadOnlySpan<char> codeBlockBody)) return;

        writer.Write(CodeBlockStartMarker.AsSpan());
        ReadOnlySpan<char> langNameValue = entireMatch.Groups[CLangId].ValueSpan;
        if (!langNameValue.IsEmpty) {
            writer.Write(CodeBlockLang.AsSpan())
                .Write(langNameValue)
                .Write('"');
        }

        writer.Write('>');
        ProcessCodeBlockContent(writer, ref codeBlockBody);
        writer.Write(CodeBlockEndMarker.AsSpan());
    }

    private static void ProcessCodeBlockContent(IMarkdownWriter writer, ref ReadOnlySpan<char> content) {
        int currentIndex = 0;

        foreach (ValueMatch match in HtmlSymbolLookup.CodeBlockRegex.EnumerateMatches(content)) {
            int matchIndex = match.Index;
            int matchLength = match.Length;
            if (currentIndex < matchIndex) {
                writer.Write(content.Slice(currentIndex, matchIndex - currentIndex));
            }

            if (HtmlSymbolLookup.CodeBlockAlternateLookup.TryGetValue(content.Slice(matchIndex, matchLength), out string? replacement)) {
                writer.Write(replacement.AsSpan());
            }

            currentIndex = matchIndex + matchLength;
        }

        if (currentIndex < content.Length) {
            writer.Write(content[currentIndex..]);
        }
    }
}
