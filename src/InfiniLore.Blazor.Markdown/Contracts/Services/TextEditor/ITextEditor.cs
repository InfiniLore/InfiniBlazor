// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.Blazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ITextEditor {
    string Text { get; set; }
    IEnumerable<ITextModifier> Modifiers { get; }
    
    void Modify(string section, Range range);
    void Insert(string input, Range range);
    
    bool TryGetCaretLine(int caretIndex, out Range lineRange);
    bool TryGetCaretUpdate(out int caretIndex);
    void UpdateCaret(int caretIndex);
}
