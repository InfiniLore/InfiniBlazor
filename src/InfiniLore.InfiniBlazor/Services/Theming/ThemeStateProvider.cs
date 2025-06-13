// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.JsRuntime;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IThemeStateProvider>]
public class ThemeStateProvider(
    IThemingConfig themingConfig,
    ILogger<ThemeStateProvider> logger,
    IJsRuntimeHelper jsRuntimeHelper,
    
    // Optional Services
    IExternalThemeCollectionProvider? externalProvider = null
) : IThemeStateProvider {
    
    public event Action? OnChanged;
    public event Func<Task>? OnChangedAsync;
    private readonly ThemeState State = new() {
        ThemeCollectionName = themingConfig.DefaultThemeCollectionName,
        ThemeModeName = themingConfig.DefaultThemeMode.Name,
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IThemeState GetState() 
        => State;
    
    public async ValueTask<bool> TrySelectCollectionAsync(string collectionName, CancellationToken ct = default) {
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Theme name is null or empty.");
        
        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");
        
        await State.SetThemeCollectionNameAsync(collection.CollectionName, ct);
        
        string? modeName = await State.TryGetThemeModeNameAsync(ct);
        if (modeName.IsNullOrWhiteSpace()) modeName = collection.GetFirstMode().Name;
        if (!collection.ContainsModeName(modeName)) modeName = collection.GetFirstMode().Name;
        await State.SetThemeModeNameAsync(modeName, ct);
        
        await OnChangedAsyncInternal();
        return true;
    }

    public async ValueTask<bool> TrySelectModeAsync(string modeName, CancellationToken ct = default) {
        if (modeName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Mode name is null or empty.");
        
        string? collectionName = await State.TryGetThemeCollectionNameAsync(ct);
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Could not find theme collection.");
        
        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");

        if (!collection.ContainsModeName(modeName)) modeName = collection.GetFirstMode().Name;
        
        await State.SetThemeModeNameAsync(modeName, ct);
        await OnChangedAsyncInternal();
        return true;
    }

    public async ValueTask<bool> TrySelectNextModeAsync(CancellationToken ct = default) {
        State.SetNextModeRequested();
        await OnChangedAsyncInternal();
        return true;
    }
    
    private async ValueTask OnChangedAsyncInternal() {
        OnChanged?.Invoke();
        if (OnChangedAsync is not null) await OnChangedAsync();
    }
    
    public async ValueTask<IThemeCollection?> TryGetCollectionAsync(string themeName, CancellationToken ct = default) {
        if (themingConfig.RegisteredBaseThemes.TryGetValue(themeName, out IThemeCollection? theme)) {
            await State.SetThemeCollectionNameAsync(theme.CollectionName, ct);
            await OnChangedAsyncInternal();
            return theme;
        }

        if (externalProvider is null) return null;
        return await externalProvider.TryGetCollectionAsync(themeName, ct);
    }
    
}
