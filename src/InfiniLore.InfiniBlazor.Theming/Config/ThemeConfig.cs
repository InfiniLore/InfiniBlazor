// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Theming.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ThemeConfig(IInfiniBlazorConfig config) : IThemeConfig{

    private readonly HashSet<string> _registeredThemes = [];
    public IReadOnlyCollection<string> RegisteredThemes  => _registeredThemes;
    public IThemeMode DefaultThemeMode { get; set; } = ThemeMode.DarkMode;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ThemeConfig RegisterTheme<TTheme>(string themeName) where TTheme : class, IThemeCollection {
        _registeredThemes.Add(themeName);
        config.Services.AddKeyedSingleton<IThemeCollection, TTheme>(themeName);
        return this;
    }
}
