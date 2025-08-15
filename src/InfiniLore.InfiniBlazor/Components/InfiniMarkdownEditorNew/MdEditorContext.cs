// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.TextEditor;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdEditorContext {
    public bool IsLocked { get; set; }
    public ITextSource TextSource { get; set; } = new TextSource();
    public ElementReference ContentRef { get; set; }

    public static MdEditorContext Empty => new();
}
