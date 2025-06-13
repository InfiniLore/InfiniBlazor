// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// --------------------------------------------------------------------------------------------------------------------
public class ThemeState : IThemeState {
    public string? ThemeCollectionName { private get; set; }
    public string? ThemeModeName { private get; set; }
    private bool NextModeRequested { get; set; }
    
    // private const string ThemeCollectionNameKey = "themeState.themeCollectionName";
    // private const string ThemeModeNameKey = "themeState.themeModeName";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask SetThemeCollectionNameAsync(string themeName, CancellationToken ct = default) {
        ThemeCollectionName = themeName;
        return ValueTask.CompletedTask;
        // await jsRuntime.LocalStorage.TrySetValueAsync(ThemeCollectionNameKey, themeName, ct: ct);
    }
    
    public ValueTask<string?> TryGetThemeCollectionNameAsync(CancellationToken ct = default) {
        if (ThemeCollectionName.IsNotNullOrWhiteSpace()) return ValueTask.FromResult<string?>(ThemeCollectionName);
        // ThemeCollectionName = await jsRuntime.LocalStorage.TryGetValueAsync<string>(ThemeCollectionNameKey, ct: ct);
        return ValueTask.FromResult(ThemeCollectionName);
    }
    
    public ValueTask SetThemeModeNameAsync(string themeName, CancellationToken ct = default) {
        ThemeModeName = themeName;
        return ValueTask.CompletedTask;
        // await jsRuntime.LocalStorage.TrySetValueAsync(ThemeModeNameKey, themeName, ct: ct);
    }
    
    public ValueTask<string?> TryGetThemeModeNameAsync(CancellationToken ct = default) {
        if (ThemeModeName.IsNotNullOrWhiteSpace()) return ValueTask.FromResult<string?>(ThemeModeName);
        // ThemeModeName = await jsRuntime.LocalStorage.TryGetValueAsync<string>(ThemeModeNameKey, ct: ct);
        return ValueTask.FromResult(ThemeModeName);
    }
    
    public void SetNextModeRequested() => NextModeRequested = true;
    
    public bool IsNextModeRequested() {
        if (!NextModeRequested) return false;
        NextModeRequested = false;
        return true;
    }
}
