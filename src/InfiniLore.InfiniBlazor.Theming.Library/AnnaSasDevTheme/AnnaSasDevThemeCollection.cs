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
            modes.Add(ThemeMode.DarkMode, AnnaSasDevTheme.DarkModeInstance);
            modes.Add(ThemeMode.LightMode, AnnaSasDevTheme.LightModeInstance);
            return modes;
        }
    }
}
