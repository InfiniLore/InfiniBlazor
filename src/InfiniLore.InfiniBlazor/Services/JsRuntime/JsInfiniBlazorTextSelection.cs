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
[InjectableScoped<IJsInfiniBlazorTextSelection>]
public class JsInfiniBlazorTextSelection(
    IJSRuntime jsRuntime,
    ILogger<JsInfiniBlazorTextSelection> logger
) : IJsInfiniBlazorTextSelection {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task<int> GetStartIndexAsync(ElementReference element) {
        try {
            return await jsRuntime.InvokeAsync<int>("infiniBlazor.textSelection.getStartIndex", element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error getting selection start");
            return -1;
        }
    }

    public async Task<int> GetEndIndexAsync(ElementReference element) {
        try {
            return await jsRuntime.InvokeAsync<int>("infiniBlazor.textSelection.getEndIndex", element);
        }
        catch (Exception e) {
            logger.Warning(e, "Error getting selection end");
            return -1;
        }
    }

    public async Task<(int, int)> GetAsTupleAsync(ElementReference element) {
        try {
            var obj = await jsRuntime.InvokeAsync<Tuple<int, int>>("infiniBlazor.textSelection.getAsTuple", element);
            return (Math.Max(0, obj.Item1), Math.Max(0, obj.Item2));
        }
        catch (Exception e) {
            logger.Warning(e, "Error getting selection");
            return (0, 0);
        }
    }

    public async Task<Range> GetRangeAsync(ElementReference element) {
        (int, int) tuple = await GetAsTupleAsync(element);
        return new Range(tuple.Item1, tuple.Item2);
    }

    public async Task SetRangeAsync(ElementReference element, int start, int end) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.textSelection.setRange", element, start, end);
        }
        catch (Exception e) {
            logger.LogWarning(e, "Error setting selection range");
        }
    }

    public async Task SetRangeAsync(ElementReference element, Range range)
        => await SetRangeAsync(element, range.Start.Value, range.End.Value);

    public async Task SetIndexAsync(ElementReference element, int index)
        => await SetRangeAsync(element, index, index);
}
