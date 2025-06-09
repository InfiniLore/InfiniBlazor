// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using JetBrains.Annotations;

namespace Example.Themes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class AnnaSasDevThemeCollection : ThemeCollection {
    protected override Dictionary<IThemeMode, ITheme> Themes { get; } = CreateThemes();
    protected override IThemeMode[] Modes { get; } = [
        TransDarkMode,
        TransLightMode,
        // BisexualDarkMode,
        // BisexualLightMode,
    ];
    
    private static IThemeMode TransDarkMode { get; } = ThemeMode.DarkMode;
    private static IThemeMode TransLightMode { get; } = ThemeMode.LightMode;
    // private static IThemeMode BisexualDarkMode { get; } = new ThemeMode("BisexualDark", true, false);
    // private static IThemeMode BisexualLightMode { get; } = new ThemeMode("BisexualLight", false, true);
    
    

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static Dictionary<IThemeMode, ITheme> CreateThemes() {
        Dictionary<IThemeMode, ITheme> modes = [];
        modes.Add(TransDarkMode, AnnaSasDevTheme.DarkModeInstance);
        modes.Add(TransLightMode, AnnaSasDevTheme.LightModeInstance);
        // modes.Add(BisexualDarkMode, BisexualTheme.DarkModeInstance);
        // modes.Add(BisexualLightMode, BisexualTheme.LightModeInstance);
        return modes;
    }
}
