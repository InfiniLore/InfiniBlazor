// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Themes.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ThemeConfig(IInfiniBlazorConfig config) : IThemeConfig{

    private readonly HashSet<string> _registeredThemes = [];
    public IReadOnlyCollection<string> RegisteredThemes  => _registeredThemes;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ThemeConfig RegisterTheme<TTheme>(string themeName) where TTheme : class, IInfiniLoreTheme {
        _registeredThemes.Add(themeName);
        config.Services.AddKeyedSingleton<IInfiniLoreTheme, TTheme>(themeName);
        return this;
    }
}
