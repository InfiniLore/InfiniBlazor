// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

// ReSharper disable once CheckNamespace
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniMarkdownEditor(
    ITextEditor textEditor,
    IJsRuntimeHelper jsRuntimeHelper,
    IMarkdownParser<string, string> markdownParser,
    IHtmlSanitizer sanitizer
) : ComponentBase {
    [Parameter, EditorRequired] public ITextSource Source { get;  set; } = null!;
    [Parameter] public EventCallback<ITextSource> SourceChanged { get; set; }
    [Parameter] public string? Class { get; init; }
    [Parameter] public bool ShowPreview { get; init; } = true;
    
    public ElementReference InputRef { get; set; } 
    public event Action? SourceHasChanged;
    public string MarkdownOutput { get; private set; } = string.Empty;
    
    private InfiniEditorKeyCombo _lastPressedCombo = InfiniEditorKeyCombo.Empty;
    private Dictionary<InfiniEditorKeyCombo, Func<InfiniMarkdownEditor, Task>> KeyCombos { get; } = new() {
        [InfiniEditorKeyCombo.Bold] = static editor => editor.OnModifierClickAsync("bold"),
        [InfiniEditorKeyCombo.Italic] = static editor => editor.OnModifierClickAsync("italic"),
        [InfiniEditorKeyCombo.Underline] = static editor => editor.OnModifierClickAsync("underline"),
        [InfiniEditorKeyCombo.SelectAll] = static editor => editor.HandleSelectAllAsync()
    };
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender) {
            // Only add the "prevent default listener" once.
            await jsRuntimeHelper.AddPreventDefaultListenerAsync();
            return;
        }

        // place the caret in a newly requested location
        if (textEditor.TryGetCaretUpdate(out int index))
            await jsRuntimeHelper.SetSelectionIndexAsync(InputRef,index);
    }

    protected override async Task OnInitializedAsync() {
        await InvokeSourceHasChanged();
    }

    protected override async Task OnParametersSetAsync() {
        await base.OnParametersSetAsync();
        await InvokeSourceHasChanged();
    }

    private async Task InvokeSourceHasChanged() {
        string? output = await markdownParser.TryParseAsync(Source.Text);
        if (output is not null) MarkdownOutput = sanitizer.Sanitize(output);
    
        SourceHasChanged?.Invoke();
        StateHasChanged();
    }

    public async ValueTask DisposeAsync() {
        await jsRuntimeHelper.RemovePreventDefaultListenerAsync();
        GC.SuppressFinalize(this);
    }
    
    public async Task OnInputAsync(ChangeEventArgs e) {
        if (e.Value is string value) {
            Source.Text = value;
            await SourceChanged.InvokeAsync(Source);
            await InvokeSourceHasChanged();
        }
    }

    public async Task OnKeyDownAsync(KeyboardEventArgs args) {
        InfiniEditorKeyCombo combo = InfiniEditorKeyCombo.From(args);
        if (KeyCombos.TryGetValue(combo, out Func<InfiniMarkdownEditor, Task>? task)) await task(this);
        _lastPressedCombo = combo;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Editor Handlers
    // -----------------------------------------------------------------------------------------------------------------
    public async Task OnModifierClickAsync(string modifierName) {
        Range range = await jsRuntimeHelper.GetSelectionRangeAsync(InputRef);
        textEditor.Modify(Source, modifierName, range);
        await InvokeSourceHasChanged();
        await SourceChanged.InvokeAsync(Source);
    }

    public async Task OnInsertClickAsync(string content) {
        Range range = await jsRuntimeHelper.GetSelectionRangeAsync(InputRef);
        textEditor.Insert(Source, content, range);
        await InvokeSourceHasChanged();
        await SourceChanged.InvokeAsync(Source);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // KeyCombo Handlers
    // -----------------------------------------------------------------------------------------------------------------
    private async Task HandleSelectAllAsync() {
        if (Source.Text.IsNullOrWhiteSpace()) return;

        Range range = new(0, Source.Length);
        
        // when we select "a" only once, we should get the current line
        if (_lastPressedCombo != InfiniEditorKeyCombo.SelectAll) {
            int caretIndex = await jsRuntimeHelper.GetSelectionStartAsync(InputRef);
            if (!textEditor.TryGetCaretLine(Source, caretIndex, out Range lineRange)) return;
            range = lineRange;
        }
        
        await jsRuntimeHelper.SetSelectionRangeAsync(InputRef, range);

    }
}
