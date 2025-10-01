// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming.CssData;
using JetBrains.Annotations;

namespace InfiniLore.InfiniBlazor.Theming.ThemeCollections;
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
            ColorBase00 = "var(--color-infini-base-100)",
            ColorBase05 = "var(--color-infini-base-95)",
            ColorBase10 = "var(--color-infini-base-90)",
            ColorBase20 = "var(--color-infini-base-80)",
            ColorBase30 = "var(--color-infini-base-70)",
            ColorBase40 = "var(--color-infini-base-60)",
            ColorBase50 = "var(--color-infini-base-50)",
            ColorBase60 = "var(--color-infini-base-40)",
            ColorBase70 = "var(--color-infini-base-30)",
            ColorBase80 = "var(--color-infini-base-20)",
            ColorBase90 = "var(--color-infini-base-10)",
            ColorBase95 = "var(--color-infini-base-05)",
            ColorBase100 = "var(--color-infini-base-00)",
        
            ButtonDefault = "var(--color-infini-base-10)",
            ButtonPrimary = "var(--color-infini-base-80)",
            
            Section = "var(--color-infini-base-10)",
        }
    };
}
