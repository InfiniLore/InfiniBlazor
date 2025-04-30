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
            ThemeData.DarkMode, InfiniBlazorTheme.Instance
        }, {
            ThemeData.LightMode, new InfiniBlazorTheme {
                ColorBase00 = InfiniBlazorTheme.Instance.ColorBase100,
                ColorBase05 = InfiniBlazorTheme.Instance.ColorBase95,
                ColorBase10 = InfiniBlazorTheme.Instance.ColorBase90,
                ColorBase20 = InfiniBlazorTheme.Instance.ColorBase80,
                ColorBase30 = InfiniBlazorTheme.Instance.ColorBase70,
                ColorBase40 = InfiniBlazorTheme.Instance.ColorBase60,
                ColorBase50 = InfiniBlazorTheme.Instance.ColorBase50,
                ColorBase60 = InfiniBlazorTheme.Instance.ColorBase40,
                ColorBase70 = InfiniBlazorTheme.Instance.ColorBase30,
                ColorBase80 = InfiniBlazorTheme.Instance.ColorBase20,
                ColorBase90 = InfiniBlazorTheme.Instance.ColorBase10,
                ColorBase95 = InfiniBlazorTheme.Instance.ColorBase05,
                ColorBase100 = InfiniBlazorTheme.Instance.ColorBase00,
            }
        }
    };
}
