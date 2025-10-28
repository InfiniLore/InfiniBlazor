// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.JsRuntime;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace InfiniLore.InfiniBlazor.Js;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IInfiniBlazorJsMermaid>]
public class InfiniBlazorJsMermaid(
    IJSRuntime jsRuntime,
    ILogger<InfiniBlazorJsMermaid> logger
) : IInfiniBlazorJsMermaid {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task RenderMermaidAsync(ElementReference element, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.mermaid.renderMermaidAsync", ct, element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error trying to render mermaid graph for element {element}", element);
        }
    }
    public async Task RenderMermaidWithContentAsync(ElementReference element, string content, CancellationToken ct = default)  {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.mermaid.renderMermaidWithContentAsync", ct, element, content);
        }
        catch (Exception e) {
            logger.Warning(e, "Error trying to render mermaid graph for element {element}", element);
        }
    }
}
