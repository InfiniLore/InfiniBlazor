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
    protected override Dictionary<IThemeData, ITheme> Themes => new() {
        {
            ThemeData.DarkMode, InfiniBlazorTheme.DarkModeInstance
        }, {
            ThemeData.LightMode, InfiniBlazorTheme.LightModeInstance
        }
    };
}
