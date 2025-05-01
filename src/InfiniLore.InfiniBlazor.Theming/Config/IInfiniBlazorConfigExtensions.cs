// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Theming.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniBlazorConfigExtensions {
    public static void AddThemingLogic(this IInfiniBlazorConfig config, Action<ThemeConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniLoreInfiniBlazorTheming();
        
        var themeConfig = new ThemeConfig(config);
        config.Services.AddKeyedSingleton<IThemeCollection, InfiniBlazorThemeCollection>(null); // Add Default theme
        
        configure?.Invoke(themeConfig);
        
        config.Services.AddSingleton<IThemeConfig>(themeConfig);
    }
}
