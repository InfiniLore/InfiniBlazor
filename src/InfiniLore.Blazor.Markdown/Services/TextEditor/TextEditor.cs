// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.Blazor.Markdown.Config;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableTransient<ITextEditor>]
public class TextEditor(IMarkdownConfig markdownConfig, IServiceProvider provider) : ITextEditor {
    private int _caretIndexToUpdate = -1;
    
    private FrozenDictionary<string, ITextModifier> ModifierLookup { get; } = markdownConfig.TextEditorModifierNames.ToFrozenDictionary(
        name => name,
        provider.GetRequiredKeyedService<ITextModifier>
    );
    private FrozenDictionary<string, ITextModifier>.AlternateLookup<ReadOnlySpan<char>>? _lookupCache;
    private FrozenDictionary<string, ITextModifier>.AlternateLookup<ReadOnlySpan<char>> AlternateLookup => _lookupCache ??=  ModifierLookup.GetAlternateLookup<ReadOnlySpan<char>>();

    public IEnumerable<ITextModifier> Modifiers => ModifierLookup.Values;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Modify(ITextSource source, string section, Range range) {
        if (!AlternateLookup.TryGetValue(section, out ITextModifier? modifier)) return;
        if (!modifier.IsSingleLineStructure || range.Start.Value == range.End.Value) {
            modifier.Modify(source, range, this);
            return;
        }

        // if it is a single line structure, then we need to treat every line as a new modifier
        int totalLength = source.Length;
        int start = range.Start.GetOffset(totalLength);
        int end = range.End.GetOffset(totalLength);

        for (int index = source.Lines.Count - 1; index >= 0; index--) {
            Range lineRange = source.Lines[index];
            int lineStart = lineRange.Start.GetOffset(totalLength);
            int lineEnd = lineRange.End.GetOffset(totalLength);

            // Skip lines outside the selected range
            if (lineEnd <= start || lineStart >= end) continue;

            // Calculate the overlapping portion of the current line with the selection
            int intersectStart = Math.Max(lineStart, start);
            int intersectEnd = Math.Min(lineEnd, end);

            // check if the line is empty
            if (source.Text.AsSpan(intersectStart, intersectEnd - intersectStart).IsEmpty) continue;
            
            // handle like lists
            Match lineMatch = TextEditorRegexLib.ListItemsRegex.Match(source.Text, intersectStart, intersectEnd - intersectStart);
            if (lineMatch.Success) {
                int newIntersectStart = intersectStart;
                if (lineMatch.Groups[1].TryGetLength(out int prefixLength)) {
                    if (lineMatch.Groups[2].TryGetValueSpan(out ReadOnlySpan<char> body) && body.Trim().IsWhiteSpace()) continue;
                    newIntersectStart += prefixLength;
                }
                modifier.Modify(source, new Range(newIntersectStart, intersectEnd), this);
                continue;
            }
            
            // handle tables
            ReadOnlySpan<char> possibleTable = source.Text.AsSpan(intersectStart, intersectEnd - intersectStart);
            if (TextEditorRegexLib.IsTableLineRegex.IsMatch(possibleTable)) {
                Regex.ValueMatchEnumerator tableCellMatches = TextEditorRegexLib.TableCellsRegex.EnumerateMatches(possibleTable);
                Stack<Range> tableCellMatchesStack = [];
                foreach (ValueMatch valueMatch in tableCellMatches) {
                    tableCellMatchesStack.Push(new Range(intersectStart + valueMatch.Index, intersectStart + valueMatch.Index + valueMatch.Length));
                }
                while (tableCellMatchesStack.TryPop(out Range result)) {
                    modifier.Modify(source, result, this); 
                }
                continue;
            }
            if (TextEditorRegexLib.IsTableHeaderLineRegex.IsMatch(possibleTable)) continue;
            
            // Handle like normal
            modifier.Modify(source, new Range(intersectStart, intersectEnd), this);
        }
    }
    
    public void Insert(ITextSource source, string input, Range range) {
        int totalLength = source.Length;
        int start = range.Start.GetOffset(totalLength);
        int end = range.End.GetOffset(totalLength);

        // Ensure the range is valid
        if (start < 0 || end > totalLength || start > end) {
            throw new ArgumentOutOfRangeException(nameof(range), "Invalid range provided for text insertion.");
        }

        // Generate the new text by replacing the specified range with the input text
        source.Text = string.Concat(source.Text.AsSpan(0, start), input, source.Text.AsSpan(end));
    }

    public bool TryGetCaretLine(ITextSource source, int caretIndex, out Range lineRange) {
        int normalizedCaretIndex = Math.Max(0, caretIndex);
        
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (Range lr in source.Lines) {
            if (normalizedCaretIndex < lr.Start.Value) continue;
            if (normalizedCaretIndex > lr.End.Value) continue;

            lineRange = lr;
            return true;
        }

        lineRange = new Range(0, 0);
        return false;
    }

    public bool TryGetCaretUpdate(out int caretIndex) {
        caretIndex = _caretIndexToUpdate;
        if (_caretIndexToUpdate < 0) return false;
        _caretIndexToUpdate = -1;
        return true;
    }
    
    public void UpdateCaret(int caretIndex) {
        if (caretIndex < 0) return;
        _caretIndexToUpdate = caretIndex;
    }
}
