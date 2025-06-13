// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IThemeProvider>]
public class ThemeProvider(
    IThemingConfig themingConfig,
    ILogger<ThemeProvider> logger,
    
    // Optional Services
    IExternalThemeCollectionProvider? externalProvider = null
) : IThemeProvider {
    
    public event Action<IThemeCollection>? ThemeChanged;
    public event Func<IThemeCollection, Task>? ThemeChangedAsync;
    
    public event Action<string?>? ModeChanged;
    public event Func<string?, Task>? ModeChangedAsync;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async ValueTask<bool> TryActivateCollectionAsync(string themeName, CancellationToken ct = default) {
        IThemeCollection collection;
        if (themingConfig.RegisteredBaseThemes.TryGetValue(themeName, out IThemeCollection? defaultCollection)) {
            collection = defaultCollection;
        }
        else if (externalProvider is not null) {
            IThemeCollection? possibleCollection = await externalProvider.TryGetCollectionAsync(themeName, ct);
            if (possibleCollection is null) {
                logger.Warning("Theme collection by the name of '{themeName}' cannot be not found.", themeName);
                return false;
            }
            collection = possibleCollection;
        }
        else {
            logger.Warning("Theme collection by the name of '{themeName}' cannot be not found.", themeName);
            return false;
        }

        ThemeChanged?.Invoke(collection);
        if (ThemeChangedAsync is not null) await ThemeChangedAsync(collection).ConfigureAwait(false);
        return true;
    }

    public async ValueTask<bool> TryActivateModeAsync(string? modeName = null, CancellationToken ct = default) {
        ModeChanged?.Invoke(modeName);
        if (ModeChangedAsync is not null) await ModeChangedAsync(modeName).ConfigureAwait(false);
        return true;
    }
}
