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
    public async Task SetTextContentAsync(ElementReference element, string text) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.element.setTextContent", element, text);
        }
        catch (Exception e) {
            logger.Warning(e, "Error setting text content");
        }
    }

    public async Task<string> GetTextContentAsync(ElementReference element) {
        try {
            return await jsRuntime.InvokeAsync<string>("infiniBlazor.element.getTextContent", element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error getting text content");
            return string.Empty;
        }
    }
    
    public async Task AddHorizontalScroll(ElementReference element, double i) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.element.addHorizontalScroll", element, i);
        }
        catch (Exception e) {
            logger.Warning(e, "Error adding horizontal scroll");
        }
    }

    public async Task ClickElement(ElementReference element) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.element.clickElement", element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error clicking element by id");
        }
    }

    public async Task ClickElementById(string id) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.element.clickElementById", id);
        }
        catch (Exception e) {
            logger.Warning(e, "Error clicking element by id");
        }
    }
}
