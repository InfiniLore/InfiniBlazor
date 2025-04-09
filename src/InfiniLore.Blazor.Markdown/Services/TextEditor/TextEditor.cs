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
    private List<Range> Lines { get; set; } = [];
    
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
        if (Lines.IsEmpty()) {
            Text = modifier.Modify(Text, range);
            return;
        }
        
        // if it is a single line structure, then we need to treat every line as a new modifier
        int totalLength = Text.Length;
        int start = range.Start.GetOffset(totalLength);
        int end = range.End.GetOffset(totalLength);
        
        string modifiedText = Text;
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
            if (modifiedText.AsSpan(intersectStart, intersectEnd - intersectStart).Trim().IsWhiteSpace()) continue;
            
            // handle special structures, like lists
            Match lineMatch = TextEditorRegexLib.ListItemsRegex.Match(modifiedText, intersectStart, intersectEnd - intersectStart);
            if (lineMatch.Success && lineMatch.Groups["listId"].TryGetLength(out int lineIdLength)) {
                intersectStart += lineIdLength;
            }

            modifiedText = modifier.Modify(modifiedText,  new Range(intersectStart, intersectEnd));
        }

        Text = modifiedText;
    }
}
