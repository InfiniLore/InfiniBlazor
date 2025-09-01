// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
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
    
    public event Func<string, Task>? OnSourceUpdate;
    public event Func<Task>? OnSyntaxTreeUpdate;
    public event Func<KeyboardEventArgs, Task>? OnInputKeyDown;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    
    public async Task InvokeSourceUpdatedAsync(string value) {
        if (OnSourceUpdate is null) return;
        await OnSourceUpdate(value).ConfigureAwait(false);
    }
    
    public async Task InvokeSyntaxTreeUpdatedAsync() {
        if (OnSyntaxTreeUpdate is null) return;
        await OnSyntaxTreeUpdate().ConfigureAwait(false);
    }

    public async Task InvokeInputKeyDownAsync(KeyboardEventArgs e) {
        if (OnInputKeyDown is null) return;
        await OnInputKeyDown(e).ConfigureAwait(false);   
    }
}
