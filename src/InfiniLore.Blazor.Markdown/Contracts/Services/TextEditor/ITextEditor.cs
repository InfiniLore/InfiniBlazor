// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.Blazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ITextEditor {
    IEnumerable<ITextModifier> Modifiers { get; }

    void Modify(ITextSource source, ReadOnlySpan<char> section, Range range);
    void Insert(ITextSource source, ReadOnlySpan<char> input, Range range);
    
    bool TryGetCaretLine(ITextSource source, int caretIndex, out Range lineRange);
    bool TryGetCaretUpdate(out int caretIndex);
    void UpdateCaret(int caretIndex);
}
