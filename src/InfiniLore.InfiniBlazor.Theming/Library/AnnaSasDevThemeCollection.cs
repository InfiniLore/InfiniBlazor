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
    protected override Dictionary<IThemeMode, ITheme> Modes => new() {
        {
            ThemeMode.DarkMode, InfiniBlazorTheme.DarkModeInstance with {
                ColorAccent = "#F5A9B8"
            }
        }, {
            ThemeMode.LightMode, InfiniBlazorTheme.LightModeInstance with {
                ColorAccent = "#5BCEFA"
            }
        }
    };
}
