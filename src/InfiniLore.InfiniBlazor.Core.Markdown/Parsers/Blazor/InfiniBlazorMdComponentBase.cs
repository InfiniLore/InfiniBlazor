// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniBlazorMdComponentBase<T> : ComponentBase
    where T : class, IMdSyntaxNode 
{
    [Parameter] public required T SyntaxNode { get; set; }
    [Inject] public IBlazorMdComponentConverter ComponentConverter { get; set; } = null!;
    [CascadingParameter] public MdRenderContext? RenderContext { get; set; }
    
    protected RenderFragment RenderChildContent() => ComponentConverter.RenderChildComponents(SyntaxNode);
}