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
    public bool ShowPreview { get; set; } = true;
    public bool ShowInput { get; set; } = true;

    public string Content {
        get => TextSource.Text;
        set => TextSource.UpdateSource(value);
    }
    
    public ITextSource TextSource { get; private set; } = new TextSource();
    public ElementReference InputElementRef { get; set; }

    public IMdSyntaxTree SyntaxTree { get; set; } = MdSyntaxTree.Empty;
    
    public event Func<Task>? OnSourceChangedAsync;
    public event Func<Task>? OnSyntaxTreeChangedAsync;
    public event Func<KeyboardEventArgs, Task>? OnInputKeyDownAsync;
    public event Func<string, Task>? OnModifierActionAsync;
    public event Func<string, Task>? OnInsertActionAsync;
    
    private ThrottledDebouncer<string> SourceChangedCallbackDebouncer { get; }
    private ThrottledDebouncer SyntaxTreeChangedCallbackDebouncer { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public MdEditorContext() {
        SourceChangedCallbackDebouncer = ThrottledDebouncer<string>.FromDelegate(async value => {
            TextSource.UpdateSource(value);
            if (OnSourceChangedAsync is null) return;
            await OnSourceChangedAsync().ConfigureAwait(false);
        });
        
        SyntaxTreeChangedCallbackDebouncer = ThrottledDebouncer.FromDelegate(async () => {
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
    
    public async Task InvokeModifierAsync(string modifierName) {
        if (OnModifierActionAsync is null) return;
        await OnModifierActionAsync(modifierName).ConfigureAwait(false);   
    }
    
    public async Task InvokeInsertAsync(string content) {
        if (OnInsertActionAsync is null) return;
        await OnInsertActionAsync(content).ConfigureAwait(false);   
    }
}
