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
            return await jsRuntime.InvokeAsync<int>("getInputSelectionStart", element);
        }
        catch (JSException e) {
            logger.LogError(e, "Error getting selection start");
            return -1;
        }
    }

    public async Task<int> GetSelectionEndAsync(ElementReference element) {
        try {
            return await jsRuntime.InvokeAsync<int>("getInputSelectionEnd", element);
        }
        catch (JSException e) {
            logger.LogError(e, "Error getting selection end");
            return -1;
        }
    }

    public async Task<(int, int)> GetSelectionAsync(ElementReference element) {
        try {
            var obj = await jsRuntime.InvokeAsync<Tuple<int, int>>("getInputSelection", element);
            return (Math.Max(0, obj.Item1), Math.Max(0, obj.Item2));
        }
        catch (JSException e) {
            logger.LogError(e, "Error getting selection");
            return (0, 0);
        }
    }

    public async Task<Range> GetSelectionRangeAsync(ElementReference element) {
        (int, int) tuple = await GetSelectionAsync(element);
        return new Range(tuple.Item1, tuple.Item2);
    }

    public async Task SetSelectionRangeAsync(ElementReference element, int start, int end) {
        try {
            await jsRuntime.InvokeVoidAsync("setInputSelectionRange", element, start, end);
        }
        catch (JSException e) {
            logger.LogWarning(e, "Error setting selection range");
        }
    }

    public async Task SetSelectionRangeAsync(ElementReference element, Range range)
        => await SetSelectionRangeAsync(element, range.Start.Value, range.End.Value);

    public async Task SetSelectionIndexAsync(ElementReference element, int index)
        => await SetSelectionRangeAsync(element, index, index);

    public async Task AddPreventDefaultListenerAsync() {
        try {
            await jsRuntime.InvokeVoidAsync("addPreventDefaultListener");
        }
        catch (JSException e) {
            logger.LogError(e, "Error adding prevent default listener");
        }
        catch (JSDisconnectedException e) {
            logger.LogDebug(e, "Prevent default listener already added or the server is in static rendering mode, usually during a reconnection.");
        }
    }

    public async Task RemovePreventDefaultListenerAsync() {
        try {
            await jsRuntime.InvokeVoidAsync("removePreventDefaultListener");
        }
        catch (JSException e) {
            logger.LogWarning(e, "failed to remove prevent default listener");
        }
        catch (JSDisconnectedException e) {
            logger.LogDebug(e, "Prevent default listener already removed or the server is in static rendering mode, usually during a reconnection.");
        }
        catch (InvalidOperationException e) {
            logger.LogDebug(e, "Prevent default listener already removed or the server is in static rendering mode, usually during a reconnection.");
        }
    }

    public async Task SetTextContentAsync(ElementReference element, string text) {
        try {
            await jsRuntime.InvokeVoidAsync("setTextContent", element, text);
        }
        catch (JSException e) {
            logger.LogError(e, "Error setting text content");
        }
    }

    public async Task<string> GetTextContentAsync(ElementReference element) {
        try {
            return await jsRuntime.InvokeAsync<string>("getTextContent", element);
        }
        catch (JSException e) {
            logger.LogError(e, "Error getting text content");
            return string.Empty;
        }
    }

    public async Task AddOrUpdateStyleElementAtHead(string id, string css) {
        try {
            await jsRuntime.InvokeVoidAsync("addOrUpdateStyleElementAtHead", id, css);
        }
        catch (JSException e) {
            logger.LogError(e, "Error adding or updating style element at head");
        }
    }

    public async Task CopyToClipboardAsync(string text) {
        try {
            await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
        catch (JSException e) {
            logger.LogError(e, "Error writing text to clipboard");
        }
    }
}
