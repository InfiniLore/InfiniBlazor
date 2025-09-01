// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Buffers;

namespace InfiniLore.InfiniBlazor.TextEditor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TextSource : ITextSource {
    public string Text { get; private set; } = string.Empty;
    public int Length { get; private set; }

    private Range[] LinesCache { get; set; } = ArrayPool<Range>.Shared.Rent(0);
    public ReadOnlySpan<char> TextSpan => Text.AsSpan();
    public ReadOnlySpan<Range> LineRanges => LinesCache.AsSpan(0, LineCount);
    
    public int LineCount { get; private set; }
    public bool IsEmpty => Text.Length == 0;

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static TextSource Empty => new(string.Empty);
    public TextSource(string? source = null) {
        UpdateSource(source ?? string.Empty);   
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void UpdateSource(string value) {
        Text = value.ReplaceLineEndings("\n");
        Length = Math.Max(0, Text.Length);

        ReadOnlySpan<char> textSpan = TextSpan;
        
        int lastLineEnd = 0;
        int newLineCount = 0;
        int index = 0;
        for (; index < Length; index++) {
            if (textSpan[index] != '\n') continue;

            if (newLineCount+1 > LinesCache.Length) UpdateLinesCache(newLineCount);
            
            LinesCache[newLineCount] = new Range(lastLineEnd, index);
            lastLineEnd = index + 1;
            newLineCount++;
        }
        if (newLineCount+1 > LinesCache.Length) UpdateLinesCache(newLineCount);
            
        LinesCache[newLineCount] = new Range(lastLineEnd, Length);
        LineCount = ++newLineCount;
    }
    private void UpdateLinesCache(int newLineCount) {
        Range[] newLinesCache = ArrayPool<Range>.Shared.Rent(Math.Max(newLineCount, 1) * 2);
        Array.Copy(LinesCache, newLinesCache, Math.Min(LinesCache.Length, newLineCount)); // Copy existing ranges
        ArrayPool<Range>.Shared.Return(LinesCache, true);
        LinesCache = newLinesCache;
    }
}
