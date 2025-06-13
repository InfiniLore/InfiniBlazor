// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IThemeState {
    ValueTask SetThemeCollectionNameAsync(string themeName, CancellationToken ct = default);
    ValueTask<string?> TryGetThemeCollectionNameAsync(CancellationToken ct = default);
    
    ValueTask SetThemeModeNameAsync(string themeName, CancellationToken ct = default);
    ValueTask<string?> TryGetThemeModeNameAsync(CancellationToken ct = default);
    
    void SetNextModeRequested();
    bool IsNextModeRequested();
}
