// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.JsRuntime;

namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// --------------------------------------------------------------------------------------------------------------------
public class ThemeState(
    IJsRuntimeHelper jsRuntime
) : IThemeState {
    public string? ThemeCollectionName { private get; set; }
    public string? ThemeModeName { private get; set; }
    public bool NextModeRequested { private get; set; }
    
    private const string ThemeCollectionNameKey = "themeState.themeCollectionName";
    private const string ThemeModeNameKey = "themeState.themeModeName";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async ValueTask SetThemeCollectionNameAsync(string themeName, CancellationToken ct = default) {
        ThemeCollectionName = themeName;
        // await jsRuntime.LocalStorage.TrySetValueAsync(ThemeCollectionNameKey, themeName, ct: ct);
    }
    
    public async ValueTask<string?> TryGetThemeCollectionNameAsync(CancellationToken ct = default) {
        if (ThemeCollectionName.IsNotNullOrWhiteSpace()) return ThemeCollectionName;
        // ThemeCollectionName = await jsRuntime.LocalStorage.TryGetValueAsync<string>(ThemeCollectionNameKey, ct: ct);
        return ThemeCollectionName;
    }
    
    public async ValueTask SetThemeModeNameAsync(string themeName, CancellationToken ct = default) {
        ThemeModeName = themeName;
        // await jsRuntime.LocalStorage.TrySetValueAsync(ThemeModeNameKey, themeName, ct: ct);
    }
    
    public async ValueTask<string?> TryGetThemeModeNameAsync(CancellationToken ct = default) {
        if (ThemeModeName.IsNotNullOrWhiteSpace()) return ThemeModeName;
        // ThemeModeName = await jsRuntime.LocalStorage.TryGetValueAsync<string>(ThemeModeNameKey, ct: ct);
        return ThemeModeName;
    }
    
    public void SetNextModeRequested() => NextModeRequested = true;
    
    public bool IsNextModeRequested() {
        if (!NextModeRequested) return false;
        NextModeRequested = false;
        return true;
    }
}
