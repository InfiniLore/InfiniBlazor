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
            ThemeData.DarkMode, new InfiniBlazorTheme { ColorAccent = "#5BCEFA" }
        }, {
            ThemeData.LightMode, new InfiniBlazorTheme { ColorAccent = "#F5A9B8" }
        }
    };
}
