// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace InfiniLore.Blazor.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IJsRuntimeHelper>]
public class JsRuntimeHelper(IJSRuntime jsRuntime) : IJsRuntimeHelper {
    public async Task<int> GetSelectionStartAsync(ElementReference element) {
        return await jsRuntime.InvokeAsync<int>("getSelectionStart", element);
    }
    public async Task<int> GetSelectionEndAsync(ElementReference element) {
        return await jsRuntime.InvokeAsync<int>("getSelectionEnd", element);
    }
}
