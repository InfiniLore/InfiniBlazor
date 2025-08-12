// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.JsRuntime;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJsInfiniBlazor {
    IJsInfiniBlazorDocument Document { get; }
    IJsInfiniBlazorElement Element { get; }
    IJsInfiniBlazorTextSelection TextSelection { get; }
    IJsInfiniBlazorKeyDownListener KeyDownListener { get; }
    
    Task CopyToClipboardAsync(string text);
}
