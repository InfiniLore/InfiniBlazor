// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;

namespace InfiniLore.InfiniBlazor.Theming.Library;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class AnnaSasDevThemeCollection : ThemeCollection {
    protected override Dictionary<IThemeMode, ITheme> Themes { get; } = CreateThemes();
    protected override IThemeMode[] Modes { get; } = [
        ThemeMode.DarkMode, 
        ThemeMode.LightMode 
    ];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static Dictionary<IThemeMode, ITheme> CreateThemes() {
        Dictionary<IThemeMode, ITheme> modes = [];
        modes.Add(ThemeMode.DarkMode, AnnaSasDevTheme.DarkModeInstance);
        modes.Add(ThemeMode.LightMode, AnnaSasDevTheme.LightModeInstance);
        return modes;
    }
}
