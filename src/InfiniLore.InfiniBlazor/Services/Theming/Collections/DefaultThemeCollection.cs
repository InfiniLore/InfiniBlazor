// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming.CssData;
using JetBrains.Annotations;

namespace InfiniLore.InfiniBlazor.Theming.Collections;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class DefaultThemeCollection : ThemeCollection {
    public const string Name = "Default";
    public override string CollectionName => Name;

    protected override OrderedDictionary<ThemeMode, ICssData> Modes { get; } = new() {
        [ThemeMode.DarkMode] = EmptyCssData.Instance,
        
        [ThemeMode.LightMode] = EmptyCssData.Instance with {
            ColorBase00 = InfiniBlazorCssData.Instance.ColorBase100,
            ColorBase05 = InfiniBlazorCssData.Instance.ColorBase95,
            ColorBase10 = InfiniBlazorCssData.Instance.ColorBase90,
            ColorBase20 = InfiniBlazorCssData.Instance.ColorBase80,
            ColorBase30 = InfiniBlazorCssData.Instance.ColorBase70,
            ColorBase40 = InfiniBlazorCssData.Instance.ColorBase60,
            ColorBase50 = InfiniBlazorCssData.Instance.ColorBase50,
            ColorBase60 = InfiniBlazorCssData.Instance.ColorBase40,
            ColorBase70 = InfiniBlazorCssData.Instance.ColorBase30,
            ColorBase80 = InfiniBlazorCssData.Instance.ColorBase20,
            ColorBase90 = InfiniBlazorCssData.Instance.ColorBase10,
            ColorBase95 = InfiniBlazorCssData.Instance.ColorBase05,
            ColorBase100 = InfiniBlazorCssData.Instance.ColorBase00,
        
            ButtonDefault = InfiniBlazorCssData.Instance.ColorBase10,
            ButtonPrimary = InfiniBlazorCssData.Instance.ColorBase80,
            
            Section = InfiniBlazorCssData.Instance.ColorBase10,
        }
    };
}
