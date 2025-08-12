// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Debugger;
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniMarkdownEditor(
    ITextEditor textEditor,
    IJsInfiniBlazor jsInfiniBlazor,
    IMdSyntaxParser syntaxParser,
    [FromKeyedServices("styled")] IMdSyntaxTreeConverter treeConverter,
    IVisualDebuggerProvider debuggerProvider,
    ILogger<InfiniMarkdownEditor> logger
    // IHtmlSanitizer sanitizer
) : InfiniComponentBase {
    [Parameter, EditorRequired] public ITextSource Source { get; set; } = null!;
    [Parameter] public EventCallback<ITextSource> SourceChanged { get; set; }

    [Parameter] public bool ShowSidePreview { get; init; } = true;

    public ElementReference InputRef { get; set; }
    public event Action? SourceHasChanged;
    public MarkupString MarkdownOutput { get; private set; }
    public string? MarkdownStringOutput { get; private set; }

    private InfiniEditorKeyCombo LastPressedCombo { get; set; } = InfiniEditorKeyCombo.Empty;
    private Dictionary<InfiniEditorKeyCombo, Func<InfiniMarkdownEditor, Task>> KeyCombos { get; } = new() {
        [InfiniEditorKeyCombo.Bold] = static editor => editor.OnModifierClickAsync("bold"),
        [InfiniEditorKeyCombo.Italic] = static editor => editor.OnModifierClickAsync("italic"),
        [InfiniEditorKeyCombo.Underline] = static editor => editor.OnModifierClickAsync("underline"),
        [InfiniEditorKeyCombo.SelectAll] = static editor => editor.HandleSelectAllAsync()
    };

    private bool EditorIsLocked { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override async Task OnAfterRenderAsync(bool firstRender) {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender) {
            // Only add the "prevent default listener" once.
            await jsInfiniBlazor.KeyDownListener.AddPreventDefaultListenerAsync();
            return;
        }

        // place the caret in a newly requested location
        if (textEditor.TryGetCaretUpdate(out int index))
            await jsInfiniBlazor.TextSelection.SetIndexAsync(InputRef, index);
    }

    protected override void OnInitialized() {
        InvokeSourceHasChanged();
        debuggerProvider.OnChange += InvokeSourceHasChanged;
    }

    protected override void OnParametersSet() {
        base.OnParametersSet();
        InvokeSourceHasChanged();
    }

    private void InvokeSourceHasChanged() {
        MdSyntaxTree tree = MdSyntaxTree.Pool.Get();
        try {
            syntaxParser.ParseToTree(Source.Text, tree);
            MarkdownOutput = treeConverter.ConvertToMarkupString(tree);
            if (debuggerProvider.IsEnabled()) MarkdownStringOutput = MarkdownOutput.ToString();

            SourceHasChanged?.Invoke();
            StateHasChanged();

        }
        finally {
            MdSyntaxTree.Pool.Return(tree);
        }
    }

    public override async ValueTask DisposeAsync() {
        await base.DisposeAsync();
        await jsInfiniBlazor.KeyDownListener.RemovePreventDefaultListenerAsync();
        debuggerProvider.OnChange -= InvokeSourceHasChanged;
        GC.SuppressFinalize(this);
    }

    public async Task OnInputAsync(ChangeEventArgs e) {
        if (EditorIsLocked) return;

        if (e.Value is string value) {
            Source.Text = value;
            await SourceChanged.InvokeAsync(Source);
            InvokeSourceHasChanged();
        }
    }

    public async Task OnKeyDownAsync(KeyboardEventArgs args) {
        if (EditorIsLocked) return;

        InfiniEditorKeyCombo combo = InfiniEditorKeyCombo.From(args);
        if (KeyCombos.TryGetValue(combo, out Func<InfiniMarkdownEditor, Task>? task)) await task(this);
        LastPressedCombo = combo;
        logger.Warning("Key combo {combo} pressed", combo);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Editor Handlers
    // -----------------------------------------------------------------------------------------------------------------
    public async Task OnModifierClickAsync(string modifierName) {
        if (EditorIsLocked) return;

        Range range = await jsInfiniBlazor.TextSelection.GetRangeAsync(InputRef);
        textEditor.Modify(Source, modifierName, range);
        InvokeSourceHasChanged();
        await SourceChanged.InvokeAsync(Source);
    }

    public async Task OnInsertClickAsync(string content) {
        if (EditorIsLocked) return;

        Range range = await jsInfiniBlazor.TextSelection.GetRangeAsync(InputRef);
        textEditor.Insert(Source, content, range);
        InvokeSourceHasChanged();
        await SourceChanged.InvokeAsync(Source);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // KeyCombo Handlers
    // -----------------------------------------------------------------------------------------------------------------
    private async Task HandleSelectAllAsync() {
        if (EditorIsLocked) return;
        if (Source.Text.IsNullOrWhiteSpace()) return;

        if (LastPressedCombo == InfiniEditorKeyCombo.SelectAll) {
            Range fullRange = new(0, Source.Length);
            await jsInfiniBlazor.TextSelection.SetRangeAsync(InputRef, fullRange);
            return;
        }
        
        // when we select "a" only once, we should get the current line
        int caretIndex = await jsInfiniBlazor.TextSelection.GetStartIndexAsync(InputRef);
        if (!textEditor.TryGetCaretLine(Source, caretIndex, out Range lineRange)) return;

        await jsInfiniBlazor.TextSelection.SetRangeAsync(InputRef, lineRange);
    }


}
