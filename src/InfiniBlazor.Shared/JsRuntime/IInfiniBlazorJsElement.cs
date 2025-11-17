// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniBlazor.JsRuntime;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IInfiniBlazorJsElement {
    Task ClearValueAsync(ElementReference element, CancellationToken ct = default);
    Task SetValueAsync(ElementReference element, string text, CancellationToken ct = default);
    Task SetValueSelectionAwareAsync(ElementReference element, string text, CancellationToken ct = default);
    
    Task SetTextContentAsync(ElementReference element, string text, CancellationToken ct = default);
    Task<string> GetTextContentAsync(ElementReference element, CancellationToken ct = default);
    Task AddHorizontalScroll(ElementReference element, double i, CancellationToken ct = default);
    Task ClickElement(ElementReference element, CancellationToken ct = default);
    Task ClickElementById(string id, CancellationToken ct = default);
}
