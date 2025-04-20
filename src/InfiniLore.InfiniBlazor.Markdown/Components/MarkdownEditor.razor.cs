// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.JsRuntime;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace InfiniLore.InfiniBlazor.Markdown.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class MarkdownEditor(ITextEditor textEditor, IJsRuntimeHelper jsRuntimeHelper, IMarkdownParser markdownParser) : ComponentBase {
    private MarkupString MarkdownOutput { get; set; }
    private ElementReference _textareaRef;
    private string _lastPressedKey = string.Empty;
    private string Text {
        get => Source.Text;
        set {
            // needed to fix a but that places the caret at the end of the text, see https://github.com/dotnet/aspnetcore/issues/20072
            Source.Text = value;
            UpdateMarkdown();
        }
    }
    
    [Parameter, EditorRequired] public required ITextSource Source { get; init; } = null!;

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

        // place the caret in newly requested location
        if (textEditor.TryGetCaretUpdate(out int index)) {
            await jsRuntimeHelper.SetSelectionRangeAsync(_textareaRef, index, index);
        }
    }
    
    public async ValueTask DisposeAsync() {
        await jsRuntimeHelper.RemovePreventDefaultListenerAsync();
        GC.SuppressFinalize(this);
    }
    
    private void UpdateMarkdown() {
        MarkdownOutput = new MarkupString(markdownParser.ParseToString(Source.Text));
    }
    
    private async Task OnModifierClickAsync(string modifierName) {
        textEditor.Modify(Source, modifierName, await GetSelectionRangeAsync());

        UpdateMarkdown();
    }
    
    private async Task<Range> GetSelectionRangeAsync() {
        (int, int) tuple = await jsRuntimeHelper.GetSelectionAsync(_textareaRef);
        return new Range(tuple.Item1, tuple.Item2);
    }
    
    private async Task OnKeyDownAsync(KeyboardEventArgs obj) {
        if (!obj.CtrlKey) return;
        string lastPressedKey = _lastPressedKey;
        _lastPressedKey = obj.Key;
        
        // Todo make this come form a dictionary or something which has the option for user configuration
        switch (obj, lastPressedKey) {
            case ({ CtrlKey: true, Key: "a" or "A" }, "a"): {
                if (Source.Text.IsNullOrWhiteSpace()) break;

                await jsRuntimeHelper.SetSelectionRangeAsync(_textareaRef, 0, Source.Length);
                break;
            }

            case ({ CtrlKey: true, Key: "a" or "A" }, _): {
                if (Source.Text.IsNullOrWhiteSpace()) break;

                // when we select a only once we should get the current line
                int caretIndex = await jsRuntimeHelper.GetSelectionStartAsync(_textareaRef);
                if (!textEditor.TryGetCaretLine(Source, caretIndex, out Range lineRange)) break;

                await jsRuntimeHelper.SetSelectionRangeAsync(_textareaRef, lineRange.Start.Value, lineRange.End.Value);
                break;
            }

            case ({ CtrlKey: true, Key: "b" }, _): {
                Range selection = await GetSelectionRangeAsync();
                textEditor.Modify(Source, "bold", selection);
                UpdateMarkdown();
                break;
            }

            case ({ CtrlKey: true, Key: "i" }, _): {
                Range selection = await GetSelectionRangeAsync();
                textEditor.Modify(Source, "italic", selection);
                UpdateMarkdown();
                break;
            }

            case ({ CtrlKey: true, Key: "u" }, _): {
                Range selection = await GetSelectionRangeAsync();
                textEditor.Modify(Source, "underline", selection);
                UpdateMarkdown();
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
        UpdateMarkdown();
    }
    
    private async Task DebugTableAsync() {
        const string tableText = """
            | test | something |
            |  ---- | --------- |
            | alpha | beta |
            """;
        
        textEditor.Insert(Source, tableText, await GetSelectionRangeAsync());
        UpdateMarkdown();
    }
}
