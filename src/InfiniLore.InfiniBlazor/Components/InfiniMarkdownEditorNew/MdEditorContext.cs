// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.Debouncers;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.TextEditor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace InfiniLore.InfiniBlazor.Components;

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
    
    private ThrottledDebouncer<string>? _sourceChangedDebouncer; 
    private ThrottledDebouncer? _syntaxTreeChangedDebouncer;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    
    public async Task InvokeSourceChangeAsync(string value) {
        if (OnSourceChangedAsync is null) {
            if (_sourceChangedDebouncer is null) return; 
            _sourceChangedDebouncer = null;
            return;
        }

        _sourceChangedDebouncer ??= OnSourceChangedAsync.GetThrottledDebouncer(100, 100);
        await _sourceChangedDebouncer.InvokeDebouncedAsync(value).ConfigureAwait(false);
    }
    
    public async Task InvokeSyntaxTreeChangeAsync() {
        if (OnSyntaxTreeChangedAsync is null) {
            if (_syntaxTreeChangedDebouncer is null) return; 
            _syntaxTreeChangedDebouncer = null;
            return;
        }

        _syntaxTreeChangedDebouncer ??= OnSyntaxTreeChangedAsync.GetThrottledDebouncer(100, 100);
        await _syntaxTreeChangedDebouncer.InvokeDebouncedAsync().ConfigureAwait(false);
    }

    public async Task InvokeInputKeyDownAsync(KeyboardEventArgs e) {
        if (OnInputKeyDownAsync is null) return;
        await OnInputKeyDownAsync(e).ConfigureAwait(false);   
    }
}
