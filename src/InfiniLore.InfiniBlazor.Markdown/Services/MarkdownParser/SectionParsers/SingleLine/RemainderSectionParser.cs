// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.MarkdownParser;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Services.SectionParsers.SingleLine;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// [InjectableSingleton<ISingleLineSectionParser>("remainder")]
public abstract class RemainderSectionParser : ISingleLineSectionParser{
    public SingleLineOrigin SkipOnOrigin => SingleLineOrigin.NotSkipped;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Obsolete("This parser is not meant to be used for direct single line parsing. Use ParseToStringBuilder(ref ReadOnlySpan<char> remainder, IMarkdownWriter writer) instead.")]
    public void ParseToStringBuilder(Match entireMatch, Group group, IMarkdownWriter writer, SingleLineOrigin origin) {
        throw new NotSupportedException("This parser is not meant to be used for direct single line parsing. Use ParseToStringBuilder(ref ReadOnlySpan<char> remainder, IMarkdownWriter writer) instead.");
    }

    public static void ParseToStringBuilder(ref ReadOnlySpan<char> remainder, IMarkdownWriter writer) {
        if (remainder.IsEmpty) return;
        int currentIndex = 0;

        foreach (ValueMatch match in HtmlSymbolLookup.Regex.EnumerateMatches(remainder)) {
            int matchIndex = match.Index;
            int matchLength = match.Length;
            if (currentIndex < matchIndex) {
                writer.Write(remainder.Slice(currentIndex, matchIndex - currentIndex));
            }
            if (HtmlSymbolLookup.AlternateLookup.TryGetValue(remainder.Slice(matchIndex, matchLength), out string? replacement)) {
                writer.Write(replacement);
            }
            currentIndex = matchIndex + matchLength;
        }
        if (currentIndex < remainder.Length) {
            writer.Write(remainder[currentIndex..]);
        }
    }

}
