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
    protected override Dictionary<IThemeMode, ITheme> Modes {
        get {
            Dictionary<IThemeMode, ITheme> modes = [];
            modes.Add(ThemeMode.DarkMode, InfiniBlazorTheme.DarkModeInstance with {
                ColorAccent = "#F5A9B8"
            });
            modes.Add(ThemeMode.LightMode, InfiniBlazorTheme.LightModeInstance with {
                ColorAccent = "#5BCEFA"
            });
            return modes;
        }
    }
}
