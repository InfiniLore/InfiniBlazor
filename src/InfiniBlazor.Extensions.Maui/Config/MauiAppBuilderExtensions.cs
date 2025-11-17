// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

// ReSharper disable once CheckNamespace
namespace InfiniBlazor.Config;
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
            configure?.Invoke(config);
        });

        // WTF why do I need to set this to null? https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui-blazor-web-app?view=aspnetcore-9.0
        // For MAUI this has to be set to null so it can overtake it as some form of an Interactive WebView?
        InfiniRenderModeProvider.InfiniRenderMode = null!;
        
        return builder;
    }
}
