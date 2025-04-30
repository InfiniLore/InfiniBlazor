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
    protected override Dictionary<IThemeData, ITheme> Themes => new() {
        {
            ThemeData.DarkMode, InfiniBlazorTheme.DarkModeInstance with {
                ColorAccent = "#5BCEFA"
            }
        }, {
            ThemeData.LightMode, InfiniBlazorTheme.LightModeInstance with {
                ColorAccent = "#F5A9B8"
            }
        }
    };
}
