// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Markdown;
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
    [Parameter] [EditorRequired] public ITextSource Source { get;  set; } = null!;
    [Parameter] public EventCallback<ITextSource> SourceChanged { get; set; }
    [Parameter] public string? Class { get; init; }
    [Parameter] public bool ShowPreview { get; init; } = true;

    public ElementReference InputRef { get; set; } 
    private string _lastPressedKey = string.Empty;
    
    public event Action? SourceHasChanged;
    public string MarkdownOutput { get; private set; } = string.Empty;
    
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
        if (textEditor.TryGetCaretUpdate(out int index)) {
            await jsRuntimeHelper.SetSelectionRangeAsync(InputRef, index, index);
        }
    }

    protected override async Task OnInitializedAsync() {
        await InvokeSourceHasChanged();
    }

    protected override async Task OnParametersSetAsync() {
        await base.OnParametersSetAsync();
        await InvokeSourceHasChanged();
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

    private async Task InvokeSourceHasChanged() {
        string? output = await markdownParser.TryParseAsync(Source.Text);
        if (output is not null) MarkdownOutput = sanitizer.Sanitize(output);
    
        SourceHasChanged?.Invoke();
        StateHasChanged();
    }

    public async Task OnModifierClickAsync(string modifierName) {
        textEditor.Modify(Source, modifierName, await GetSelectionRangeAsync());
        await InvokeSourceHasChanged();
        await SourceChanged.InvokeAsync(Source);
    }

    private async Task<Range> GetSelectionRangeAsync() {
        (int, int) tuple = await jsRuntimeHelper.GetSelectionAsync(InputRef);
        return new Range(tuple.Item1, tuple.Item2);
    }

    public async Task OnKeyDownAsync(KeyboardEventArgs obj) {
        if (!obj.CtrlKey) return;

        string lastPressedKey = _lastPressedKey;
        _lastPressedKey = obj.Key;

        // Todo make this come form a dictionary or something which has the option for user configuration
        switch (obj, lastPressedKey) {
            case ({ CtrlKey: true, Key: "a" or "A" }, "a"): {
                if (Source.Text.IsNullOrWhiteSpace()) break;

                await jsRuntimeHelper.SetSelectionRangeAsync(InputRef, 0, Source.Length);
                break;
            }

            case ({ CtrlKey: true, Key: "a" or "A" }, _): {
                if (Source.Text.IsNullOrWhiteSpace()) break;

                // when we select a only once we should get the current line
                int caretIndex = await jsRuntimeHelper.GetSelectionStartAsync(InputRef);
                if (!textEditor.TryGetCaretLine(Source, caretIndex, out Range lineRange)) break;

                await jsRuntimeHelper.SetSelectionRangeAsync(InputRef, lineRange.Start.Value, lineRange.End.Value);
                break;
            }

            case ({ CtrlKey: true, Key: "b" }, _): {
                Range selection = await GetSelectionRangeAsync();
                textEditor.Modify(Source, "bold", selection);
                await InvokeSourceHasChanged();
                break;
            }

            case ({ CtrlKey: true, Key: "i" }, _): {
                Range selection = await GetSelectionRangeAsync();
                textEditor.Modify(Source, "italic", selection);
                await InvokeSourceHasChanged();
                break;
            }

            case ({ CtrlKey: true, Key: "u" }, _): {
                Range selection = await GetSelectionRangeAsync();
                textEditor.Modify(Source, "underline", selection);
                await InvokeSourceHasChanged();
                break;
            }
        }
    }

    private async Task DebugLoremAsync() {
        const string loremText = """
            Lorem ipsum dolor
            sit amet, consectetur
            adipiscing elit. Sed
            do eiusmod tempor
            incididunt ut labore
            et dolore magna aliqua.

            Ut enim ad minim veniam,
            quis nostrud exercitation
            ullamco laboris nisi ut
            aliquip ex ea commodo
            consequat.

            Duis aute irure dolor
            in reprehenderit in 
            voluptate velit esse
            cillum dolore eu fugiat
            nulla pariatur.

            Excepteur sint occaecat
            cupidatat non proident, 
            sunt in culpa qui officia
            deserunt mollit anim id
            est laborum.
            """;

        textEditor.Insert(Source, loremText, await GetSelectionRangeAsync());
        await InvokeSourceHasChanged();
        await SourceChanged.InvokeAsync(Source);
    }

    private async Task DebugTableAsync() {
        const string tableText = """
            | test  | something |
            |  ---- | --------- |
            | alpha | beta      |
            """;

        textEditor.Insert(Source, tableText, await GetSelectionRangeAsync());
        await InvokeSourceHasChanged();
        await SourceChanged.InvokeAsync(Source);
    }
}
