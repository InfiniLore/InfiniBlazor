// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;

namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class InfiniBlazorThemeCollection : ThemeCollection {
    protected override Dictionary<IThemeMode, ITheme> Themes { get; } = CreateThemes();
    
    protected override IThemeMode[] Modes { get; } = [
        ThemeMode.DarkMode,
        ThemeMode.LightMode 
    ];

    private static Dictionary<IThemeMode, ITheme> CreateThemes() {
        Dictionary<IThemeMode, ITheme> modes = [];
        modes.Add(ThemeMode.LightMode, InfiniBlazorTheme.LightModeInstance);
        modes.Add(ThemeMode.DarkMode, InfiniBlazorTheme.DarkModeInstance);
        return modes;
    }
}
