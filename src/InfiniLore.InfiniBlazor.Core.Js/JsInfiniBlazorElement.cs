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
[InjectableScoped<IJsInfiniBlazorElement>]
public class JsInfiniBlazorElement(
    IJSRuntime jsRuntime,
    ILogger<JsInfiniBlazorElement> logger
) : IJsInfiniBlazorElement {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task SetValueAsync(ElementReference element, string text, CancellationToken ct = default) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.elements.setValue", ct, element, text);
        }
        catch (Exception e) {
            logger.Warning(e, "Error setting value {text} for element {element}", text, element);
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
