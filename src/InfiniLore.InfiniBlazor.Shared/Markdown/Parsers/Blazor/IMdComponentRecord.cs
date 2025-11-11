// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.AspNetCore.Components.Rendering;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdComponentRecord {
    Type ComponentType { get; }
    Func<RenderTreeBuilder, int, IMdSyntaxNode, int> Builder { get; }
}
