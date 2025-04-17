// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.TextEditor;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TextSource : ITextSource{
    private string _text = string.Empty;
    public string Text {
        get => _text;
        set {
            _text = TextEditorRegexLib.LineEndingRegex.Replace(value, "\n");
            UpdateTextMetaData();
        }
    }
    
    public ReadOnlySpan<char> TextSpan => Text.AsSpan();
    public int Length { get; private set; }
    
    private List<Range> LinesCache { get; } = [];
    public IReadOnlyList<Range> Lines => LinesCache.AsReadOnly();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private void UpdateTextMetaData() {
        CacheLineRanges();
        
        Length = Math.Max(0, Text.Length);
    }
    
    private void CacheLineRanges() {
        Regex.ValueMatchEnumerator lineMatches = TextEditorRegexLib.NewlinesRegex.EnumerateMatches(Text);
        LinesCache.Clear();

        int lastIndex = 0;
        int textLength = Text.Length;
        
        foreach (ValueMatch valueMatch in lineMatches) {
            lastIndex = valueMatch.Index + valueMatch.Length;
            LinesCache.Add(new Range(valueMatch.Index, lastIndex - 1)); // -1 to exclude the newline character
        }

        // add the last line which didn't end with a newline
        if (lastIndex < textLength) {
            LinesCache.Add(new Range(lastIndex, textLength));
        }
    }
}
