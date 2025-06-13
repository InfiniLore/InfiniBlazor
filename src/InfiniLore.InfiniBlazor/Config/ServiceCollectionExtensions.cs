// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using Infinilore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Theming.Collections;
using System.Collections.Frozen;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddInfiniBlazor(this IServiceCollection services, Action<InfiniBlazorConfig>? configure = null) {
        var config = new InfiniBlazorConfig(services);
        services.RegisterServicesFromInfiniLoreInfiniBlazor();
        services.AddLucideIcons();

        config.RegisterTheme<DefaultThemeCollection>(DefaultThemeCollection.Name);

        configure?.Invoke(config);
        
        // Add all added stuff after the config is done
        services.AddSingleton<IThemingConfig>(new FrozenThemingConfig {
            RegisteredBaseThemes = config.RegisteredBaseThemeCollections.ToFrozenDictionary(),
            DefaultThemeCollectionName = config.DefaultThemeCollectionName,
            DefaultThemeMode = config.DefaultThemeMode,
        });

        return services;
    }
}
