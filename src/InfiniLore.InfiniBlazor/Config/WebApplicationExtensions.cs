// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class WebApplicationExtensions {
    public static WebApplication UseInfiniBlazor(this WebApplication app) {
        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        lifetime.ApplicationStarted.Register(() => {
            _ = Task.Run(async () => {
                try {
                    var emoteProvider = app.Services.GetRequiredService<IEmoteProvider>();
                    await emoteProvider.InitializeAsync();
                }
                catch (Exception ex) {
                    var logger = app.Services.GetRequiredService<ILogger<WebApplication>>();
                    logger.LogError(ex, "Failed to initialize EmoteProvider");
                }
            });
        });
        return app;
    }
}
