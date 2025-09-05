using InfiniLore.InfiniBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Markdown;
// -----------------------------------------------------------------------------------------------------------------
// Methods
// -----------------------------------------------------------------------------------------------------------------
public partial class InfiniMarkdownPreview : InfiniComponentBase {
    [CascadingParameter] public MarkdownEditorState EditorState { get; set; } = null!; 

    private string? MarkdownStringOutput => EditorState.MarkdownStringOutput;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnInitialized() {
        base.OnInitialized();
        EditorState.OnOutputChange += StateHasChanged;
    }

    public override async ValueTask DisposeAsync() {
        await base.DisposeAsync();
        
        EditorState.OnOutputChange -= StateHasChanged;
        GC.SuppressFinalize(this);
    }
}
