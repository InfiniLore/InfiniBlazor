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
[InjectableScoped<IInfiniBlazorJsElement>]
public class InfiniBlazorJsElement(
    IJSRuntime jsRuntime,
    ILogger<InfiniBlazorJsElement> logger
) : IInfiniBlazorJsElement {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task ClearValueAsync(ElementReference element, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.elements.clearValue", ct, element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error clearing value for element {element}", element);
        }
    }
    
    public async Task SetValueAsync(ElementReference element, string text, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.elements.setValue", ct, element, text);
        }
        catch (Exception e) {
            logger.Warning(e, "Error setting value {text} for element {element}", text, element);
        }
    }
    
    public async Task SetValueSelectionAwareAsync(ElementReference element, string text, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.elements.setValueSelectionAware", ct, element, text);
        }
        catch (Exception e) {
            logger.Warning(e, "Error setting text content");
        }
    }
    
    public async Task SetTextContentAsync(ElementReference element, string text, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.elements.setTextContent", ct, element, text);
        }
        catch (Exception e) {
            logger.Warning(e, "Error setting text content");
        }
    }

    public async Task<string> GetTextContentAsync(ElementReference element, CancellationToken ct = default) {
        try {
            return await jsRuntime.InvokeAsync<string>("infiniBlazor.elements.getTextContent", ct, element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error getting text content");
            return string.Empty;
        }
    }
    
    public async Task AddHorizontalScroll(ElementReference element, double i, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.elements.addHorizontalScroll", ct, element, i);
        }
        catch (Exception e) {
            logger.Warning(e, "Error adding horizontal scroll");
        }
    }

    public async Task ClickElement(ElementReference element, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.elements.clickElement", ct, element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error clicking element by id");
        }
    }

    public async Task ClickElementById(string id, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.elements.clickElementById", ct, id);
        }
        catch (Exception e) {
            logger.Warning(e, "Error clicking element by id");
        }
    }
}
