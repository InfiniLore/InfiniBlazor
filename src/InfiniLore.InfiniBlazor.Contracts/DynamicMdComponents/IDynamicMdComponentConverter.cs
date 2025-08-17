// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.DynamicMdComponents;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IDynamicMdComponentConverter {
    RenderFragment RenderComponent(IMdSyntaxNode node);
    RenderFragment RenderComponentDebug(IMdSyntaxNode node);
    
    RenderFragment RenderChildComponents(IMdSyntaxNode node);
    RenderFragment RenderRootComponents(IEnumerable<IMdSyntaxNode> nodes);
}
