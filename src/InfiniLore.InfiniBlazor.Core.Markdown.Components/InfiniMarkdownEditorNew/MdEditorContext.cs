// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Debouncers;
using InfiniLore.InfiniBlazor.Markdown.Editors;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdEditorContext {
    public bool IsLocked { get; set; }
    public ITextSource TextSource { get; private set; } = new TextSource();
    public ElementReference InputElementRef { get; set; }

    public IMdSyntaxTree SyntaxTree { get; set; } = MdSyntaxTree.Empty;
    
    public event Func<string, Task>? OnSourceChangedAsync;
    public event Func<Task>? OnSyntaxTreeChangedAsync;
    public event Func<KeyboardEventArgs, Task>? OnInputKeyDownAsync;
    
    private Debouncer<string> SourceChangedCallbackDebouncer { get; }
    private Debouncer SyntaxTreeChangedCallbackDebouncer { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public MdEditorContext() {
        SourceChangedCallbackDebouncer = Debouncer<string>.FromDelegate(async input => {
            if (OnSourceChangedAsync is null) return;
            await OnSourceChangedAsync(input).ConfigureAwait(false);
        });
        
        SyntaxTreeChangedCallbackDebouncer = Debouncer.FromDelegate(async () => {
            if (OnSyntaxTreeChangedAsync is null) return;
            await OnSyntaxTreeChangedAsync().ConfigureAwait(false);
        });
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task InvokeSourceChangeAsync(string value) 
        => await SourceChangedCallbackDebouncer.InvokeDebouncedAsync(value);
    
    public async Task InvokeSyntaxTreeChangeAsync()
        => await SyntaxTreeChangedCallbackDebouncer.InvokeDebouncedAsync();

    public async Task InvokeInputKeyDownAsync(KeyboardEventArgs e) {
        if (OnInputKeyDownAsync is null) return;
        await OnInputKeyDownAsync(e).ConfigureAwait(false);   
    }
}
