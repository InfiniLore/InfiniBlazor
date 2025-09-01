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
    [CascadingParameter] protected MdEditorContext EditorContext { get; set; } = null!;
    [CascadingParameter] protected MdRenderContext RenderContext { get; set; } = null!;
}
