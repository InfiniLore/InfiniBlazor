// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniMdEditorComponentBase : InfiniComponentBase {
    [CascadingParameter] protected MdEditorContext EditorContext { get; set; } = MdEditorContext.Empty;
    [CascadingParameter] protected MdRenderContext RenderContext { get; set; } = MdRenderContext.Empty;
    
    // Some helpful quick handles for the EditorContext
    protected bool IsLocked => EditorContext.IsLocked;
    protected ITextSource TextSource => EditorContext.TextSource;
}
