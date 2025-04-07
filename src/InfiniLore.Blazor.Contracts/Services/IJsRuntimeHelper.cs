// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.Blazor.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJsRuntimeHelper {
    Task<int> GetSelectionStartAsync(ElementReference element);
    Task<int> GetSelectionEndAsync(ElementReference element);
    Task PreventDefaultAsync<T>(T sender) where T: EventArgs;
    Task SetSelectionRangeAsync(ElementReference element, int start, int end);
}
