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
            ColorInfiniBase00 = "#000000", 
            ColorInfiniBase05 = "#090b0a", 
            ColorInfiniBase10 = "#111413", 
            ColorInfiniBase20 = "#1a1e1d", 
            ColorInfiniBase30 = "#222826", 
            ColorInfiniBase40 = "#2b3230", 
            ColorInfiniBase50 = "#333c39", 
            ColorInfiniBase60 = "#555c5a", 
            ColorInfiniBase70 = "#767d7a", 
            ColorInfiniBase80 = "#989d9b", 
            ColorInfiniBase90 = "#b9bdbc", 
            ColorInfiniBase95 = "#dbdddc", 
            ColorInfiniBase100 = "#ffffff", 
        
            ColorInfiniButtonDefault = "var(--color-infini-base-10)",
            ColorInfiniButtonPrimary = "var(--color-infini-base-80)",
        }
    };
}
