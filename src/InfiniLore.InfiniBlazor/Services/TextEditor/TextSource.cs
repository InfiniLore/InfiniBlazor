// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.TextEditor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TextSource : ITextSource {
    private string _text = string.Empty;
    public string Text {
        get => _text;
        set {
            _text = TextEditorRegexLib.LineEndingRegex.Replace(value, "\n");
            Length = Math.Max(0, _text.Length);
            CacheLineRanges();
        }
    }

    public ReadOnlySpan<char> TextSpan => Text.AsSpan();
    public int Length { get; private set; }

    private List<Range> LinesCache { get; } = new(16);
    public IReadOnlyList<Range> Lines => LinesCache.AsReadOnly();
    public bool IsEmpty => _text.Length == 0;
    
    public static TextSource Empty => new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private void CacheLineRanges() {
        Regex.ValueSplitEnumerator lineMatches = TextEditorRegexLib.LineEndingRegex.EnumerateSplits(Text);
        LinesCache.Clear();

        foreach (Range valueMatch in lineMatches) {
            LinesCache.Add(valueMatch);// -1 to exclude the newline character
        }
    }
}
