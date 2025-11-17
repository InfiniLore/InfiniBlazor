// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using Microsoft.AspNetCore.Components.Rendering;

namespace InfiniBlazor.Markdown.Parsers.Blazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdComponentRecord {
    Type ComponentType { get; }
    Func<RenderTreeBuilder, int, IMdSyntaxNode, int> Builder { get; }
}
