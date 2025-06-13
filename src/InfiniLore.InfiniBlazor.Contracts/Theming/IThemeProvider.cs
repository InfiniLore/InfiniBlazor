// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IThemeProvider {
    event Action<IThemeCollection>? ThemeChanged;
    event Func<IThemeCollection, Task>? ThemeChangedAsync;
    event Action<string?>? ModeChanged;
    event Func<string?, Task>? ModeChangedAsync;
     
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    ValueTask<bool> TryActivateCollectionAsync(string themeName, CancellationToken ct = default);
    ValueTask<bool> TryActivateModeAsync(string? modeName = null, CancellationToken ct = default);
}
