// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Debouncers;
using InfiniLore.InfiniBlazor.TextEditor;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MarkdownEditorState {
    public bool ShowSidePreview => true;
    public bool IsLocked { get; set; }
    
    public ElementReference InputRef { get; set; }
    
    public ITextSource Source { get; set; } = new TextSource();
    public string? MarkdownStringOutput { get; set; }
    public MarkupString MarkdownOutput { get; set; }

    public event Action? OnOutputChange;
    
    private Debouncer SourceChangedCallbackDebouncer { get; set; } = Debouncer.Empty;
    
    public void OutputHasChanged() => OnOutputChange?.Invoke();
    
    public void SetSourceChangedCallback(Action? callback) {
        if (callback is null) {
            SourceChangedCallbackDebouncer = Debouncer.Empty;
            return;
        }
        SourceChangedCallbackDebouncer = callback.GetDebouncer(100);
    }
    
    public async Task SourceHasChanged() => await SourceChangedCallbackDebouncer.InvokeDebouncedAsync();
}
