// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Theming;

namespace Infinilore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ThemeConfig : IThemeConfig{
    internal readonly HashSet<string> RegisteredThemesSet = [];
    public IReadOnlyCollection<string> RegisteredThemes  => RegisteredThemesSet;
    public IThemeMode DefaultThemeMode { get; set; } = ThemeMode.DarkMode;
}
