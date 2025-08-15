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
    public ITextSource TextSource { get; private set; } = new TextSource();
    public ElementReference ContentRef { get; set; }
    public MdEditorOutput Output { get; set; }
    
    public event Func<Task>? OnSourceChange; 
    
    public static MdEditorContext Empty => new();

    // -----------------------------------------------------------------------------------------------------------------
    // Constuctors
    // -----------------------------------------------------------------------------------------------------------------
    private MdEditorContext() { }
    public MdEditorContext Initialize() {
        
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void SetTextSource(ITextSource textSource)
        => TextSource = textSource;
    
    public async Task UpdateSource(string value) {
        TextSource.Text = value;
        
        if (OnSourceChange is null) return;
        await OnSourceChange().ConfigureAwait(false);
    }
}
