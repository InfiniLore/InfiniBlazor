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
            ColorInfiniBase00 = "var(--color-infini-base-100)",
            ColorInfiniBase05 = "var(--color-infini-base-95)",
            ColorInfiniBase10 = "var(--color-infini-base-90)",
            ColorInfiniBase20 = "var(--color-infini-base-80)",
            ColorInfiniBase30 = "var(--color-infini-base-70)",
            ColorInfiniBase40 = "var(--color-infini-base-60)",
            ColorInfiniBase50 = "var(--color-infini-base-50)",
            ColorInfiniBase60 = "var(--color-infini-base-40)",
            ColorInfiniBase70 = "var(--color-infini-base-30)",
            ColorInfiniBase80 = "var(--color-infini-base-20)",
            ColorInfiniBase90 = "var(--color-infini-base-10)",
            ColorInfiniBase95 = "var(--color-infini-base-05)",
            ColorInfiniBase100 = "var(--color-infini-base-00)",
        
            ColorInfiniButtonDefault = "var(--color-infini-base-10)",
            ColorInfiniButtonPrimary = "var(--color-infini-base-80)",
            
            ColorInfiniSection = "var(--color-infini-base-10)",
        }
    };
}
