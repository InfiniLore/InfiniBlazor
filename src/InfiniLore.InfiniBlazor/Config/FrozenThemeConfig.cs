// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;

namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class FrozenThemingConfig : IThemingConfig{
    public required IReadOnlyDictionary<string, IThemeCollection> RegisteredBaseThemes { get; init; }
    public required string DefaultThemeCollectionName { get; init; }
    public required ThemeMode DefaultThemeMode { get; init; }
}
