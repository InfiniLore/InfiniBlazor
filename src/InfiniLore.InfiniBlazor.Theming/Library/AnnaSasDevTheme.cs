// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming.Library;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class AnnaSasDevTheme : ThemeCollection {
    protected override Dictionary<IThemeData, ITheme> Themes => new() {
        {
            ThemeData.DarkMode, new InfiniBlazorTheme { ColorAccent = "#5BCEFA" }
        }, {
            ThemeData.LightMode, new InfiniBlazorTheme { ColorAccent = "#F5A9B8" }
        }
    };
}
