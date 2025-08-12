// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace InfiniLore.InfiniBlazor.JsRuntime;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IJsInfiniBlazorDocument>]
public class JsInfiniBlazorDocument(
    IJSRuntime jsRuntime,
    ILogger<JsInfiniBlazorDocument> logger
) : IJsInfiniBlazorDocument {

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
