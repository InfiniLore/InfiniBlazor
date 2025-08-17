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
    RenderFragment RenderChildComponents(IMdSyntaxNode node);
    RenderFragment RenderRootComponents(IEnumerable<IMdSyntaxNode> nodes);
}
