// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IBlazorMdComponentConverter {
    RenderFragment RenderComponent(IMdSyntaxNode node);
    RenderFragment RenderComponentDebug(IMdSyntaxNode node);
    
    RenderFragment RenderChildComponents(IMdSyntaxNode node);
    RenderFragment RenderRootComponents(IEnumerable<IMdSyntaxNode> nodes);
}
