// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.JsRuntime;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace InfiniLore.InfiniBlazor.Js;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IJsInfiniBlazor>]
public class JsInfiniBlazor(
    IJSRuntime jsRuntime,
    ILogger<JsInfiniBlazor> logger,
    
    IJsInfiniBlazorDocument documentService,
    IJsInfiniBlazorElement elementService,
    IJsInfiniBlazorTextSelection textSelectionService,
    IJsInfiniBlazorKeyDownListener keyDownListenerService
) : IJsInfiniBlazor {
    public IJsInfiniBlazorDocument Document => documentService;
    public IJsInfiniBlazorElement Element => elementService;
    public IJsInfiniBlazorTextSelection TextSelection => textSelectionService;
    public IJsInfiniBlazorKeyDownListener KeyDownListener => keyDownListenerService;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task CopyToClipboardAsync(string text) {
        try {
            await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
        catch (Exception e) {
            logger.Warning(e, "Error writing text to clipboard");
        }
    }
}
