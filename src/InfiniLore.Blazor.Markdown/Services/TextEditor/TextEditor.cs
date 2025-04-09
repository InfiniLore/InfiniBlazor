// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.Blazor.Markdown.Config;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableTransient<ITextEditor>]
public class TextEditor(IMarkdownConfig markdownConfig, IServiceProvider provider) : ITextEditor {
    private string _text = string.Empty;
    public string Text {
        get => _text;
        set => UpdateText(value); // done so we can easily bind this to the textAreaInput Component
    }
    private readonly List<Range> Lines = [];

    private FrozenDictionary<string, ITextModifier> ModifierLookup { get; } = markdownConfig.TextEditorModifierNames.ToFrozenDictionary(
        name => name,
        provider.GetRequiredKeyedService<ITextModifier>
    );

    public IEnumerable<ITextModifier> Modifiers => ModifierLookup.Values;
    private int _caretIndexToUpdate = -1;
    
    private FrozenDictionary<string, ITextModifier>.AlternateLookup<ReadOnlySpan<char>>? _lookupCache;
    private FrozenDictionary<string, ITextModifier>.AlternateLookup<ReadOnlySpan<char>> AlternateLookup => _lookupCache ??=  ModifierLookup.GetAlternateLookup<ReadOnlySpan<char>>();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private void UpdateText(string value) {
        _text = TextEditorRegexLib.LineEndingRegex.Replace(value, "\n");

        Regex.ValueMatchEnumerator lineMatches = TextEditorRegexLib.NewlinesRegex.EnumerateMatches(_text);
        Lines.Clear();

        int lastIndex = 0;
        foreach (ValueMatch valueMatch in lineMatches) {
            lastIndex = valueMatch.Index + valueMatch.Length;
            Lines.Add(new Range(valueMatch.Index, lastIndex - 1));
        }

        // add the last line which didnt end with a newline
        if (lastIndex < _text.Length) {
            Lines.Add(new Range(lastIndex, _text.Length));
        }
        _caretIndexToUpdate = -1;
    }

    public void Modify(ReadOnlySpan<char> section, Range range) {
        if (!AlternateLookup.TryGetValue(section, out ITextModifier? modifier)) return;

        if (!modifier.IsSingleLineStructure) {
            Text = modifier.Modify(Text, range, this);
            return;
        }

        if (range.Start.Value == range.End.Value) {
            Text = modifier.Modify(Text, range, this);
            return;
        }

        if (Lines.IsEmpty()) {
            Text = modifier.Modify(Text, range, this);
            return;
        }

        // if it is a single line structure, then we need to treat every line as a new modifier
        int totalLength = Text.Length;
        int start = range.Start.GetOffset(totalLength);
        int end = range.End.GetOffset(totalLength);

        for (int index = Lines.Count - 1; index >= 0; index--) {
            Range lineRange = Lines[index];
            int lineStart = lineRange.Start.GetOffset(totalLength);
            int lineEnd = lineRange.End.GetOffset(totalLength);

            // Skip lines outside the selected range
            if (lineEnd <= start || lineStart >= end) continue;

            // Calculate the overlapping portion of the current line with the selection
            int intersectStart = Math.Max(lineStart, start);
            int intersectEnd = Math.Min(lineEnd, end);

            // check if the line is empty
            if (Text.AsSpan(intersectStart, intersectEnd - intersectStart).Trim().IsWhiteSpace()) continue;

            // handle special structures, like lists
            Match lineMatch = TextEditorRegexLib.ListItemsRegex.Match(Text, intersectStart, intersectEnd - intersectStart);
            if (lineMatch.Success && lineMatch.Groups["a"].TryGetLength(out int prefixLength)) {
                if (lineMatch.Groups["b"].TryGetValueSpan(out ReadOnlySpan<char> body) && body.Trim().IsWhiteSpace()) continue;

                intersectStart += prefixLength;
            }

            Text = modifier.Modify(Text, new Range(intersectStart, intersectEnd), this);
        }
    }
    
    public void Insert(string input, Range range) {
        int totalLength = Text.Length;
        int start = range.Start.GetOffset(totalLength);
        int end = range.End.GetOffset(totalLength);

        // Ensure the range is valid
        if (start < 0 || end > totalLength || start > end) {
            throw new ArgumentOutOfRangeException(nameof(range), "Invalid range provided for text insertion.");
        }

        // Generate the new text by replacing the specified range with the input text
        var builder = new StringBuilder(_text);
        builder.Remove(start, end - start);
        builder.Insert(start, input);
        UpdateText(builder.ToString());
    }

    public bool TryGetCaretLine(int caretIndex, out Range lineRange) {
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (Range lr in Lines) {
            if (caretIndex < lr.Start.Value) continue;
            if (caretIndex > lr.End.Value) continue;

            lineRange = lr;
            return true;
        }

        lineRange = new Range(-1, -1);
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
