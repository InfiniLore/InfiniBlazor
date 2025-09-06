// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Components.WebAssembly.Hosting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class WebAssemblyHostExtensions {
    public static async Task<WebAssemblyHost> UseInfiniBlazorAsync(this WebAssemblyHost host) {
        // In WASM, we can initialize immediately since there's no server startup delay
        try {
            var emoteProvider = host.Services.GetRequiredService<IEmoteProvider>();
            await emoteProvider.InitializeAsync();
        }
        catch (Exception ex) {
            var logger = host.Services.GetRequiredService<ILogger<WebAssemblyHost>>();
            logger.LogError(ex, "Failed to initialize EmoteProvider");
        }

        return host;
    }
}
