// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Infinilore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Theming;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniBlazorConfig(IServiceCollection collection) {
    public IServiceCollection Services { get; } = collection;
    public ThemeConfig ThemeConfig { get; } = new();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniBlazorConfig RegisterTheme<TTheme>(string themeName) where TTheme : class, IThemeCollection {
        ThemeConfig.RegisteredThemesSet.Add(themeName);
        Services.AddKeyedSingleton<IThemeCollection, TTheme>(themeName);
        return this;
    }

    public InfiniBlazorConfig RegisterTheme(Type themeType, string themeName) {
        ThemeConfig.RegisteredThemesSet.Add(themeName);
        Services.AddKeyedSingleton(typeof(IThemeCollection), themeName, themeType);
        return this;
    }
}
