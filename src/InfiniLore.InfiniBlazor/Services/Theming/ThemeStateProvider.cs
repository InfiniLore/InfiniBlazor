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
[InjectableScoped<IThemeStateProvider>]
public class ThemeStateProvider(
    IThemingConfig themingConfig,
    ILogger<ThemeStateProvider> logger,
    IThemeState state,
    
    // Optional Services
    IExternalThemeCollectionProvider? externalProvider = null
) : IThemeStateProvider {
    
    public event Action? OnChanged;
    public event Func<Task>? OnChangedAsync;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IThemeState GetState() 
        => state;
    
    public async ValueTask<bool> TrySelectCollectionAsync(string collectionName, CancellationToken ct = default) {
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Theme name is null or empty.");
        
        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");
        
        await state.SetThemeCollectionNameAsync(collection.CollectionName, ct);
        await OnChangedAsyncInternal();
        return true;
    }

    public async ValueTask<bool> TrySelectModeAsync(string modeName, CancellationToken ct = default) {
        if (modeName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Mode name is null or empty.");
        
        string? collectionName = await state.TryGetThemeCollectionNameAsync(ct);
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Could not find theme collection.");
        
        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");

        if (!collection.ContainsModeName(modeName)) modeName = collection.GetFirstMode().Name;
        
        await state.SetThemeModeNameAsync(modeName, ct);
        await OnChangedAsyncInternal();
        return true;
    }

    public async ValueTask<bool> TrySelectNextModeAsync(CancellationToken ct = default) {
        state.SetNextModeRequested();
        await OnChangedAsyncInternal();
        return true;
    }
    
    private async ValueTask OnChangedAsyncInternal() {
        OnChanged?.Invoke();
        if (OnChangedAsync is not null) await OnChangedAsync();
    }
    
    public async ValueTask<IThemeCollection?> TryGetCollectionAsync(string themeName, CancellationToken ct = default) {
        if (themingConfig.RegisteredBaseThemes.TryGetValue(themeName, out IThemeCollection? theme)) {
            await state.SetThemeCollectionNameAsync(theme.CollectionName, ct);
            await OnChangedAsyncInternal();
            return theme;
        }

        if (externalProvider is null) return null;
        return await externalProvider.TryGetCollectionAsync(themeName, ct);
    }
    
}
