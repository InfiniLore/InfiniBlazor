// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniBlazor.JsRuntime;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IInfiniBlazorJsMermaid {
    Task RenderMermaidAsync(ElementReference element, CancellationToken ct = default);
    Task RenderMermaidWithContentAsync(ElementReference element, string content, CancellationToken ct = default);
}
