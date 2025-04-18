// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.JsRuntime;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace InfiniLore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IJsRuntimeHelper>]
public class JsRuntimeHelper(IJSRuntime jsRuntime, ILogger<JsRuntimeHelper> logger) : IJsRuntimeHelper {
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
            return (obj.Item1, obj.Item2);
        }
        catch (JSException e) {
            logger.LogError(e, "Error getting selection");
            return (-1, -1);
        }
    }

    public async Task SetSelectionRangeAsync(ElementReference element, int start, int end) {
        try {
            await jsRuntime.InvokeVoidAsync("setInputSelectionRange", element, start, end);
        }
        catch (JSException e) {
            logger.LogWarning(e, "Error setting selection range");
        }
    }
    
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
}
