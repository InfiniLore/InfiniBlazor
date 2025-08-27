// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Debugger;
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Markdown.Parsers.HtmlString;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.TextEditor;
using Microsoft.AspNetCore.Components;
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
    IMdStringMdSyntaxSerializer syntaxParser,
    [FromKeyedServices("styled")] IHtmlStringMdSyntaxTreeParser htmlStringTreeParser,
    IVisualDebuggerProvider debuggerProvider,
    ILogger<InfiniMarkdownEditor> logger
) : InfiniComponentBase {

    [Parameter, EditorRequired] public ITextSource Source { get; set; } = new TextSource();
    private MarkdownEditorState EditorState { get; set; } = new();
    
    private InfiniEditorKeyCombo LastPressedCombo { get; set; } = InfiniEditorKeyCombo.Empty;

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
            await jsInfiniBlazor.TextSelection.SetIndexAsync(EditorState.InputRef, index);
    }

    protected override void OnInitialized() {
        InvokeSourceHasChanged();
        debuggerProvider.OnChange += InvokeSourceHasChanged;
    }

    protected override async Task OnParametersSetAsync() {
        base.OnParametersSet();
        
        EditorState.Source = Source;
        EditorState.SetSourceChangedCallback(InvokeSourceHasChanged);
        await EditorState.SourceHasChanged();
    }

    private void InvokeSourceHasChanged() {
        InvokeAsync(() => {
            MdSyntaxTree tree = MdSyntaxTree.Pool.Get();
            try {
                syntaxParser.SerializeToTree(EditorState.Source.Text, tree);
                EditorState.MarkdownOutput = new MarkupString(htmlStringTreeParser.DeserializeToString(tree));
                if (debuggerProvider.IsEnabled()) EditorState.MarkdownStringOutput = EditorState.MarkdownOutput.ToString();

                EditorState.OutputHasChanged();
                StateHasChanged();
            }
            catch (Exception ex) {
                logger.Error(ex, "Error parsing markdown");
            }
            finally {
                MdSyntaxTree.Pool.Return(tree);
            }
        });
    }


    public override async ValueTask DisposeAsync() {
        await base.DisposeAsync();
        await jsInfiniBlazor.KeyDownListener.RemovePreventDefaultListenerAsync();
        debuggerProvider.OnChange -= InvokeSourceHasChanged;
        EditorState.SetSourceChangedCallback(null);
        GC.SuppressFinalize(this);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Editor Handlers
    // -----------------------------------------------------------------------------------------------------------------
    public async Task OnModifierClickAsync(string modifierName) {
        if (EditorState.IsLocked) return;

        Range range = await jsInfiniBlazor.TextSelection.GetRangeAsync(EditorState.InputRef);
        textEditor.Modify(EditorState.Source, modifierName, range);
        await EditorState.SourceHasChanged();
    }

    public async Task OnInsertClickAsync(string content) {
        if (EditorState.IsLocked) return;

        Range range = await jsInfiniBlazor.TextSelection.GetRangeAsync(EditorState.InputRef);
        textEditor.Insert(EditorState.Source, content, range);
        await EditorState.SourceHasChanged();
    }

    // -----------------------------------------------------------------------------------------------------------------
    // KeyCombo Handlers
    // -----------------------------------------------------------------------------------------------------------------
    public async Task HandleSelectAllAsync() {
        if (EditorState.IsLocked) return;
        if (EditorState.Source.IsEmpty) return;

        // when ctrl+a has been pressed twice, select the full text
        if (LastPressedCombo == InfiniEditorKeyCombo.SelectAll) {
            Range fullRange = new(0, EditorState.Source.Length);
            await jsInfiniBlazor.TextSelection.SetRangeAsync(EditorState.InputRef, fullRange);
            return;
        }
        
        // when ctrl+a has been pressed only once, select the current line
        int caretIndex = await jsInfiniBlazor.TextSelection.GetStartIndexAsync(EditorState.InputRef);
        if (!textEditor.TryGetCaretLine(EditorState.Source, caretIndex, out Range lineRange)) return;

        await jsInfiniBlazor.TextSelection.SetRangeAsync(EditorState.InputRef, lineRange);
    }
}
