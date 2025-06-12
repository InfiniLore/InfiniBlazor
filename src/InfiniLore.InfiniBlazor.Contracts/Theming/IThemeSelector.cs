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
    Task<bool> TrySelectThemeCollectionAsync(string themeName);
    Task<bool> TrySelectNextThemeModeAsync();
    
    bool TryGetCurrentThemeModeCss([NotNullWhen(true)] out string? css);
    bool TryGetCurrentTheme([NotNullWhen(true)] out ITheme? theme);
    bool TryCreateCssString(ITheme theme, [NotNullWhen(true)] out string? css);
}
