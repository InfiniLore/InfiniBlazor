// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniBlazor.JsRuntime;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace InfiniBlazor.Js;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IInfiniBlazorJsHighlight>]
public class InfiniBlazorJsHighlight(
    IJSRuntime jsRuntime,
    ILogger<InfiniBlazorJsHighlight> logger
) : IInfiniBlazorJsHighlight {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task HighlightElementAsync(ElementReference element, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.highlight.highlightElement", ct, element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error trying to code highlight element {element}", element);
        }
    }
    public async Task SetContentAndHighlightElementAsync(ElementReference element, string content, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.highlight.setContentAndHighlight", ct, element, content);
        }
        catch (Exception e) {
            logger.Warning(e, "Error trying to code highlight element {element}", element);
        }
    }
}
