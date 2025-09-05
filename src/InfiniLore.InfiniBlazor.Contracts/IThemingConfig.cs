// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IThemingConfig {
    string DefaultThemeCollectionName { get; }
    ThemeMode DefaultThemeMode { get; }
    
    FrozenDictionary<string, IThemeCollection> GetRegisteredThemeCollections();
}
