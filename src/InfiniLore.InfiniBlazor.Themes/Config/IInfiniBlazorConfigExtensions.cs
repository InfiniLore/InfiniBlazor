// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Themes.Library;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Themes.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class IInfiniBlazorConfigExtensions {
    public static void AddThemes(this IInfiniBlazorConfig config, Action<ThemeConfig>? configure = null) {
        config.Services.RegisterServicesFromInfiniLoreInfiniBlazorThemes();
        
        var themeConfig = new ThemeConfig(config);
        themeConfig.RegisterTheme<DefaultTheme>("default");
        
        configure?.Invoke(themeConfig);
        
        config.Services.AddSingleton<IThemeConfig>(themeConfig);
    }
}
