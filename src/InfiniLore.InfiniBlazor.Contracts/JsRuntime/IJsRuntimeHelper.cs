// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.JsRuntime;

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
    
    Task SetTextContentAsync(ElementReference element, string text);
    Task<string> GetTextContentAsync(ElementReference element);
    
    Task AddOrUpdateStyleElementAtHead(string id, string css);
    
    Task CopyToClipboardAsync(string text);
}
