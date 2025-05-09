// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IThemeSelector {
    event Func<Task>? ThemeChangedAsync;
    IThemeCollection CurrentThemeCollection { get; }
    IThemeMode CurrentThemeMode { get; }
     
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    Task<bool> TrySelectThemeAsync(string themeName);
    Task<bool> TryToggleDarkAndLightModeAsync();
    
    bool TryGetCurrentThemeCss([NotNullWhen(true)] out string? css);
}
