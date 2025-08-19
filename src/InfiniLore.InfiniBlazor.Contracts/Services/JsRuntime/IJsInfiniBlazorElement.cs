// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.JsRuntime;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJsInfiniBlazorElement {
    Task SetTextContentAsync(ElementReference element, string text);
    Task<string> GetTextContentAsync(ElementReference element);
    Task AddHorizontalScroll(ElementReference element, double i);
    Task ClickElement(ElementReference element);
    Task ClickElementById(string id);
}
