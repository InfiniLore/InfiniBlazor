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
[InjectableScoped<IJsInfiniBlazorKeyDownListener>]
public class JsInfiniBlazorKeyDownListener(
    IJSRuntime jsRuntime,
    ILogger<JsInfiniBlazorKeyDownListener> logger
) : IJsInfiniBlazorKeyDownListener {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task AddPreventDefaultListenerAsync() {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.keyListener.addPreventDefaultListener");
        }
        catch (JSDisconnectedException e) {
            logger.Debug(e, "Prevent default listener already added or the server is in static rendering mode, usually during a reconnection.");
        }
        catch (Exception e) {
            logger.Warning(e, "Error adding prevent default listener");
        }
    }

    public async Task RemovePreventDefaultListenerAsync() {
        try {
            await jsRuntime.InvokeVoidAsync("infiniBlazor.keyListener.removePreventDefaultListener");
        }
        catch (JSDisconnectedException e) {
            logger.Debug(e, "Prevent default listener already removed or the server is in static rendering mode, usually during a reconnection.");
        }
        catch (InvalidOperationException e) {
            logger.Debug(e, "Prevent default listener already removed or the server is in static rendering mode, usually during a reconnection.");
        }
        catch (Exception e) {
            logger.Warning(e, "failed to remove prevent default listener");
        }
    }
}
