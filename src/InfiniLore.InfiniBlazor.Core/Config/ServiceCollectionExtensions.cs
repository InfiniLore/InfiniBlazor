// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Core;
using InfiniLore.InfiniBlazor.Theming;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniBlazor(this IServiceCollection services, Action<InfiniBlazorConfig>? configure = null) {
        var config = new InfiniBlazorConfig(services);
        services.RegisterServicesFromInfiniLoreInfiniBlazorCore();
        services.AddLucideIcons();
        
        services.AddKeyedSingleton<IThemeCollection, InfiniBlazorThemeCollection>(null); // Add Default theme

        configure?.Invoke(config);
        
        // Add all added stuff after the config is done
        services.AddSingleton<IThemeConfig>(config.ThemeConfig);

        return services;
    }
}
