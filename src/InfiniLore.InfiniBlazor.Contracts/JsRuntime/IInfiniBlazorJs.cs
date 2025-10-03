// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.JsRuntime;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IInfiniBlazorJs {
    IInfiniBlazorJsDocument Document { get; }
    IInfiniBlazorJsElement Element { get; }
    IInfiniBlazorJsTextSelection TextSelection { get; }
    IInfiniBlazorJsKeyDownListener KeyDownListener { get; }
    IInfiniBlazorJsHighlight Highlight { get; }
    
    Task CopyToClipboardAsync(string text);
}
