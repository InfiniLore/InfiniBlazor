// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

// ReSharper disable once CheckNamespace
namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Provides extension methods for configuring and extending the Maui app builder
/// with InfiniBlazor functionality.
/// </summary>
public static class MauiAppBuilderExtensions {
    /// Adds InfiniBlazor services and configurations to the Maui application builder.
    /// This extension method configures InfiniBlazor and allows customizing its settings using an action.
    /// <param name="builder">
    /// The Maui application builder to which InfiniBlazor services are added.
    /// </param>
    /// <param name="configure">
    /// An optional action to configure additional settings for InfiniBlazor.
    /// </param>
    /// <return>
    /// The updated Maui application builder.
    /// </return>
    public static MauiAppBuilder AddInfiniBlazor(this MauiAppBuilder builder, Action<InfiniBlazorConfig>? configure = null) {
        builder.Services.AddInfiniBlazor(config => {
            config.SetRenderModeForMauiBlazorHybrid();
            configure?.Invoke(config);
        });

        return builder;
    }
}
