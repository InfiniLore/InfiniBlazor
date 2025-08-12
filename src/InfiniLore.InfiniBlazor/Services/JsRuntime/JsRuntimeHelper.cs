// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace InfiniLore.InfiniBlazor.JsRuntime;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IJsRuntimeHelper>]
public class JsRuntimeHelper(
    IJSRuntime jsRuntime,
    ILogger<JsRuntimeHelper> logger,
    IJsLocalStorageHelper localStorageHelper
) : IJsRuntimeHelper {
    public IJsLocalStorageHelper LocalStorage { get; } = localStorageHelper;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task<int> GetSelectionStartAsync(ElementReference element) {
        try {
            return await jsRuntime.InvokeAsync<int>("infiniBlazor.inputElement.getSelectionStart", element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error getting selection start");
            return -1;
        }
    }

    public async Task<int> GetSelectionEndAsync(ElementReference element) {
        try {
            return await jsRuntime.InvokeAsync<int>("infiniBlazor.inputElement.getSelectionEnd", element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error getting selection end");
            return -1;
        }
    }

    public async Task<(int, int)> GetSelectionAsync(ElementReference element) {
        try {
            var obj = await jsRuntime.InvokeAsync<Tuple<int, int>>("infiniBlazor.inputElement.getSelection", element);
            return (Math.Max(0, obj.Item1), Math.Max(0, obj.Item2));
        }
        catch (Exception e) {
            logger.Warning(e, "Error getting selection");
            return (0, 0);
        }
    }

    public async Task<Range> GetSelectionRangeAsync(ElementReference element) {
        (int, int) tuple = await GetSelectionAsync(element);
        return new Range(tuple.Item1, tuple.Item2);
    }

    public async Task SetSelectionRangeAsync(ElementReference element, int start, int end) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.inputElement.setSelectionRange", element, start, end);
        }
        catch (Exception e) {
            logger.LogWarning(e, "Error setting selection range");
        }
    }

    public async Task SetSelectionRangeAsync(ElementReference element, Range range)
        => await SetSelectionRangeAsync(element, range.Start.Value, range.End.Value);

    public async Task SetSelectionIndexAsync(ElementReference element, int index)
        => await SetSelectionRangeAsync(element, index, index);

    public async Task AddPreventDefaultListenerAsync() {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.keyListener.addPreventDefaultListener");
        }
        catch (JSDisconnectedException e) {
            logger.Debug(e, "Prevent default listener already added or the server is in static rendering mode, usually during a reconnection.");
        }
        catch (Exception e) {
            logger.Warning(e, "Error adding prevent default listener");
        }
    }

    public async Task RemovePreventDefaultListenerAsync() {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.keyListener.removePreventDefaultListener");
        }
        catch (JSDisconnectedException e) {
            logger.Debug(e, "Prevent default listener already removed or the server is in static rendering mode, usually during a reconnection.");
        }
        catch (InvalidOperationException e) {
            logger.Debug(e, "Prevent default listener already removed or the server is in static rendering mode, usually during a reconnection.");
        }
        catch (Exception e) {
            logger.LogWarning(e, "failed to remove prevent default listener");
        }
    }

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

    public async Task AddOrUpdateStyleElementAtHead(string id, string css) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.document.addOrUpdateElementAtHead", id, css);
        }
        catch (Exception e) {
            logger.Warning(e, "Error adding or updating style element at head");
        }
    }

    public async Task CopyToClipboardAsync(string text) {
        try {
            await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
        catch (Exception e) {
            logger.Warning(e, "Error writing text to clipboard");
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

    public async Task ClickElementById(string id) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.element.clickElementById", id);
        }
        catch (Exception e) {
            logger.Warning(e, "Error clicking element by id");
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
}
