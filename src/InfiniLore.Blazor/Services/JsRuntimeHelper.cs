// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace InfiniLore.Blazor.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IJsRuntimeHelper>]
public class JsRuntimeHelper(IJSRuntime jsRuntime, ILogger<JsRuntimeHelper> logger) : IJsRuntimeHelper {
    private bool SystemInvoked { get; set; } = false;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
   
    public async Task<int> GetSelectionStartAsync(ElementReference element) {
        try {
            return await jsRuntime.InvokeAsync<int>("getSelectionStart", element);
        }
        catch (JSException e) {
            logger.LogError(e, "Error getting selection start");
            return -1;
        }
    }

    public async Task<int> GetSelectionEndAsync(ElementReference element) {
        return await jsRuntime.InvokeAsync<int>("getSelectionEnd", element);
    }

    public async Task PreventDefaultAsync<T>(T sender) where T : EventArgs {
        await jsRuntime.InvokeVoidAsync("preventDefault", sender);
    }

    public async Task SetSelectionRangeAsync(ElementReference element, int start, int end) {
        await jsRuntime.InvokeVoidAsync("setSelectionRange", element, start, end);
    }
}
