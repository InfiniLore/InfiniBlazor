// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Debouncers;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.TextEditor;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MarkdownEditorState {
    public bool ShowSidePreview { get; } = true;
    public bool IsLocked { get; set; }
    
    public ElementReference InputRef { get; set; }
    
    public ITextSource Source { get; set; } = new TextSource();
    public string? MarkdownStringOutput { get; set; }
    public MarkupString MarkdownOutput { get; set; }

    public event Action? OnOutputChange;
    
    private ActionDebouncer SourceChangedCallbackDebouncer { get; set; } = ((Action)(() => {})).GetDebouncer(100);
    
    public void OutputHasChanged() => OnOutputChange?.Invoke();
    
    public void SetSourceChangedCallback(Action? callback) {
        if (callback is null) {
            SourceChangedCallbackDebouncer = ((Action)(() => {})).GetDebouncer(100);
            return;
        }
        SourceChangedCallbackDebouncer = callback.GetDebouncer(100);
    }
    
    public async Task SourceHasChanged() => await SourceChangedCallbackDebouncer.InvokeDebouncedAsync();
}
