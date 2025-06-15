// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Components.ToastAppearances;
using Infinilore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.Collections;
using InfiniLore.InfiniBlazor.Toasting;
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

        config.RegisterTheme<DefaultThemeCollection>();

        configure?.Invoke(config);
        
        // Add all added stuff after the config is done
        services.AddSingleton<IThemingConfig>(new FrozenThemingConfig {
            RegisteredBaseThemes = config.RegisteredBaseThemeCollections.ToFrozenDictionary(),
            DefaultThemeCollectionName = config.DefaultThemeCollectionName,
            DefaultThemeMode = config.DefaultThemeMode,
        });

        services.AddSingleton<IToastingConfig>(new FrozenToastingConfig {
            AutoRemoveDuration = 5000,
            AppearanceComponentMapping = new Dictionary<ToastAppearance, Type> {
                [ToastAppearance.Default] = typeof(ToastMessageBase),
                [ToastAppearance.Info] = typeof(InfoToastMessage),
                [ToastAppearance.Success] = typeof(SuccessToastMessage),
                [ToastAppearance.Warning] = typeof(WarningToastMessage),
                [ToastAppearance.Error] = typeof(ErrorToastMessage),
            }
        });

        return services;
    }
}
