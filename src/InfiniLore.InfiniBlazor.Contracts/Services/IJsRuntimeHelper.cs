// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJsRuntimeHelper {
    Task<int> GetSelectionStartAsync(ElementReference element);
    Task<int> GetSelectionEndAsync(ElementReference element);
    Task<(int, int)> GetSelectionAsync(ElementReference element);
    Task SetSelectionRangeAsync(ElementReference element, int start, int end);
    
    Task AddPreventDefaultListenerAsync();
    Task RemovePreventDefaultListenerAsync();
}
