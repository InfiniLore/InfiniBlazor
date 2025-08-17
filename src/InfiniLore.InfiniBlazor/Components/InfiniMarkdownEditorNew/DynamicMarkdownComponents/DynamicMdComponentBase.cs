// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.DynamicMdComponents;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components.DynamicMarkdownComponents;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class DynamicMdComponentBase<T> : ComponentBase
    where T : class, IMdSyntaxNode 
{
    [Parameter] public required T SyntaxNode { get; set; }
    [Inject] public IDynamicMdComponentConverter ComponentConverter { get; set; } = null!;
}