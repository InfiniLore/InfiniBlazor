// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components.Web;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record InfiniEditorKeyCombo(
    bool CtrlKeyPressed,
    string Key
) {
    public static InfiniEditorKeyCombo Empty { get; } = new(false, string.Empty);
    public static InfiniEditorKeyCombo Bold { get; } = new(true, "b");
    public static InfiniEditorKeyCombo Italic { get; } = new(true, "i");
    public static InfiniEditorKeyCombo Underline { get; } = new(true, "u"); 
    public static InfiniEditorKeyCombo SelectAll { get; } = new(true, "a");
    
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static InfiniEditorKeyCombo From(KeyboardEventArgs args) 
        => new(args.CtrlKey, args.Key.ToLowerInvariant());
}
