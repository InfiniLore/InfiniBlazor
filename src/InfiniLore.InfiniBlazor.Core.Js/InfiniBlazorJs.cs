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
[InjectableScoped<IInfiniBlazorJs>]
public class InfiniBlazorJs(
    IJSRuntime jsRuntime,
    ILogger<InfiniBlazorJs> logger,
    
    IInfiniBlazorJsDocument documentService,
    IInfiniBlazorJsElement elementService,
    IInfiniBlazorJsTextSelection textSelectionService,
    IInfiniBlazorJsKeyDownListener keyDownListenerService,
    IInfiniBlazorJsHighlight highlightService
) : IInfiniBlazorJs {
    public IInfiniBlazorJsDocument Document => documentService;
    public IInfiniBlazorJsElement Element => elementService;
    public IInfiniBlazorJsTextSelection TextSelection => textSelectionService;
    public IInfiniBlazorJsKeyDownListener KeyDownListener => keyDownListenerService;
    public IInfiniBlazorJsHighlight Highlight => highlightService;

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
