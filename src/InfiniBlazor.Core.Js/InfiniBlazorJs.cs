// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniBlazor.JsRuntime;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace InfiniBlazor.Js;
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
    IInfiniBlazorJsHighlight highlightService,
    IInfiniBlazorJsMermaid mermaidService
) : IInfiniBlazorJs {
    public IInfiniBlazorJsDocument Document => documentService;
    public IInfiniBlazorJsElement Element => elementService;
    public IInfiniBlazorJsTextSelection TextSelection => textSelectionService;
    public IInfiniBlazorJsKeyDownListener KeyDownListener => keyDownListenerService;
    public IInfiniBlazorJsHighlight Highlight => highlightService;
    public IInfiniBlazorJsMermaid Mermaid => mermaidService;

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
