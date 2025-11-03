// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.JsRuntime;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IInfiniBlazorJsHighlight {
    Task HighlightElementAsync(ElementReference element, CancellationToken ct = default);
    Task SetContentAndHighlightElementAsync(ElementReference element, string content, CancellationToken ct = default);
}
