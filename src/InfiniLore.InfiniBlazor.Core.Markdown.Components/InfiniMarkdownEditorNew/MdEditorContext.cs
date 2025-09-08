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
    public bool IsInteractive { get; set; }
    public bool ShowPreview { get; set; } = true;
    public bool ShowInput { get; set; } = true;

    public string Content {
        get => TextSource.Text;
        set => TextSource.UpdateSource(value);
    }
    
    public ITextSource TextSource { get; private set; } = new TextSource();
    public ElementReference InputElementRef { get; set; }

    public IMdSyntaxTree SyntaxTree { get; set; } = MdSyntaxTree.Empty;
    
    public event Func<string, Task>? OnSourceChangedAsync;
    public event Func<Task>? OnSyntaxTreeChangedAsync;
    public event Func<KeyboardEventArgs, Task>? OnInputKeyDownAsync;
    
    private ThrottledDebouncer<string> SourceChangedCallbackDebouncer { get; }
    private ThrottledDebouncer SyntaxTreeChangedCallbackDebouncer { get; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public MdEditorContext() {
        SourceChangedCallbackDebouncer = ThrottledDebouncer<string>.FromDelegate(async input => {
            if (OnSourceChangedAsync is null) return;
            await OnSourceChangedAsync(input).ConfigureAwait(false);
        });
        
        SyntaxTreeChangedCallbackDebouncer = ThrottledDebouncer.FromDelegate(async () => {
            if (OnSyntaxTreeChangedAsync is null) return;
            await OnSyntaxTreeChangedAsync().ConfigureAwait(false);
        });
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    internal bool IsNotInteractive() => IsLocked || !IsInteractive;
    
    public async Task InvokeSourceChangeAsync(string value) 
        => await SourceChangedCallbackDebouncer.InvokeDebouncedAsync(value);
    
    public async Task InvokeSyntaxTreeChangeAsync()
        => await SyntaxTreeChangedCallbackDebouncer.InvokeDebouncedAsync();

    public async Task InvokeInputKeyDownAsync(KeyboardEventArgs e) {
        if (OnInputKeyDownAsync is null) return;
        await OnInputKeyDownAsync(e).ConfigureAwait(false);   
    }
}
