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
    public async Task<int> GetSelectionStartAsync(ElementReference element)
        => await jsRuntime.InvokeAsync<int>("getSelectionStart", element);

    public async Task<int> GetSelectionEndAsync(ElementReference element) 
        => await jsRuntime.InvokeAsync<int>("getSelectionEnd", element);

    public async Task PreventDefaultAsync<T>(T sender) where T : EventArgs
        => await jsRuntime.InvokeVoidAsync("preventDefault", sender);

    public async Task SetSelectionRangeAsync(ElementReference element, int start, int end)
        => await jsRuntime.InvokeVoidAsync("setSelectionRangeBlazor", element, start, end);

}
