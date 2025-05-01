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
    protected override Dictionary<IThemeMode, ITheme> Modes {
        get {
            Dictionary<IThemeMode, ITheme> modes = [];
            modes.Add(ThemeMode.LightMode, InfiniBlazorTheme.LightModeInstance);
            modes.Add(ThemeMode.DarkMode, InfiniBlazorTheme.DarkModeInstance);
            return modes;
        }
    }
}
