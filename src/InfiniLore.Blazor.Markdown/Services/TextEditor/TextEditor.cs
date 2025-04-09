// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextEditor>]
public class TextEditor(IServiceProvider provider) : ITextEditor {
    private string _text = string.Empty;
    public string Text {
        get => _text;
        set => UpdateText(value);
    }
    private readonly List<Range> Lines = [];
    
    private FrozenDictionary<string, ITextModifier> ModifierLookup { get; } = ModifiersKeys.ToFrozenDictionary(
        name => name,
        provider.GetRequiredKeyedService<ITextModifier>
    );
    private static string[] ModifiersKeys { get; } = [
        "bold",
        "italic",
        "underline",
    ];
    public IEnumerable<ITextModifier> Modifiers => ModifierLookup.Values;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private void UpdateText(string value) {
        _text = value;

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
    }
    
    public void Modify(string section, Range range) {
        if (!ModifierLookup.TryGetValue(section, out ITextModifier? modifier)) return;
        if (!modifier.IsSingleLineStructure) {
            Text = modifier.Modify(Text, range);
            return;
        }
        if (range.Start.Value == range.End.Value) {
            Text = modifier.Modify(Text, range);
            return;
        }
        if (Lines.IsEmpty()) {
            Text = modifier.Modify(Text, range);
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

            Text = modifier.Modify(Text,  new Range(intersectStart, intersectEnd));
        }
    }

    public bool TryGetCaretLine(int caretIndex, out Range lineRange) {
        foreach (Range lr in Lines) {
            if (caretIndex < lr.Start.Value) continue;
            if (caretIndex > lr.End.Value) continue;
            lineRange = lr;
            return true;
        }
        lineRange = new Range(0, 0);
        return false;
    }
}
