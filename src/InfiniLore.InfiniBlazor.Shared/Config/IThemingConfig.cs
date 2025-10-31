// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;

namespace InfiniLore.InfiniBlazor.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IThemingConfig {
    (string CollectionName, string ThemeName) DefaultThemeSelection { get; }
    bool ThrowOnBrokenInitialization { get; }
    
    ThemeResource[] GetThemeResources();
}
