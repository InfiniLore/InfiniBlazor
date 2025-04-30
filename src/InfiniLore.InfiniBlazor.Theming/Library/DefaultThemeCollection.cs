// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;

namespace InfiniLore.InfiniBlazor.Theming.Library;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class DefaultThemeCollection : ThemeCollection {
    protected override Dictionary<IThemeMode, ITheme> Modes => new() {
        {
            ThemeMode.DarkMode, InfiniBlazorTheme.DarkModeInstance
        }, {
            ThemeMode.LightMode, InfiniBlazorTheme.LightModeInstance
        }
    };
}
