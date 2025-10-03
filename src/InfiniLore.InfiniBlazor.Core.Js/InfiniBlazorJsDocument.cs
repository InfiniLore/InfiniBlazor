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
[InjectableScoped<IInfiniBlazorJsDocument>]
public class InfiniBlazorJsDocument(
    IJSRuntime jsRuntime,
    ILogger<InfiniBlazorJsDocument> logger
) : IInfiniBlazorJsDocument {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task AddOrUpdateElementAtHead(string id, string css) {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.document.addOrUpdateElementAtHead", id, css);
        }
        catch (Exception e) {
            logger.Warning(e, "Error adding or updating style element at head");
        }
    }
    
}
