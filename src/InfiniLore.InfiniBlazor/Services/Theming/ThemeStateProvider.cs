// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.QueryParameters;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IThemeStateProvider>]
public class ThemeStateProvider(
    IThemingConfig themingConfig,
    ILogger<ThemeStateProvider> logger,
    IQueryParameterManager queryParameterManager,

    // Optional Services
    IExternalThemeCollectionProvider? externalProvider = null
) : IThemeStateProvider {
    
    public event Action? OnChanged;
    public event Func<Task>? OnChangedAsync;
    private readonly ThemeState State = new() {
        ThemeCollectionName = themingConfig.DefaultThemeCollectionName,
        ThemeModeName = themingConfig.DefaultThemeMode.Name
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IThemeState GetState()
        => State;

    public async ValueTask InitializeFromQueryAsync(CancellationToken ct = default) {
        string? collectionName = queryParameterManager.GetParam<string>(QueryParameterNames.ThemeCollection);
        string? modeName = queryParameterManager.GetParam<string>(QueryParameterNames.ThemeMode);

        if (!collectionName.IsNullOrWhiteSpace()) await TrySelectCollectionInternalAsync(collectionName, ct);
        if (!modeName.IsNullOrWhiteSpace()) await TrySelectModeInternalAsync(modeName, ct);
    }

    public async ValueTask<bool> TrySelectCollectionAsync(string collectionName, CancellationToken ct = default) {
        if (!await TrySelectCollectionInternalAsync(collectionName, ct)) return false;
        
        queryParameterManager.SetParams(
            (QueryParameterNames.ThemeCollection, State.ThemeCollectionName),
            (QueryParameterNames.ThemeMode, State.ThemeModeName)
        );
        await OnChangedAsyncInternal();
        return true;
    }

    private async ValueTask<bool> TrySelectCollectionInternalAsync(string collectionName, CancellationToken ct) {
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Theme name is null or empty.");

        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");

        State.ThemeCollectionName = collection.CollectionName;

        string? modeName = State.ThemeModeName;
        if (modeName.IsNullOrWhiteSpace()) modeName = collection.GetFirstMode().Name;
        if (!collection.ContainsModeName(modeName)) modeName = collection.GetFirstMode().Name;
         State.ThemeModeName = modeName;

        return true;
    }

    public async ValueTask<bool> TrySelectModeAsync(string modeName, CancellationToken ct = default) {
        if (!await TrySelectModeInternalAsync(modeName, ct)) return false;
        
        queryParameterManager.SetParams(
            (QueryParameterNames.ThemeCollection, State.ThemeCollectionName),
            (QueryParameterNames.ThemeMode, State.ThemeModeName)
        );
        await OnChangedAsyncInternal();
        return true;
    }

    private async ValueTask<bool> TrySelectModeInternalAsync(string modeName, CancellationToken ct) {
        if (modeName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Mode name is null or empty.");

        string? collectionName = State.ThemeCollectionName;
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Could not find theme collection.");

        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");

        if (!collection.ContainsModeName(modeName)) modeName = collection.GetFirstMode().Name;

         State.ThemeModeName = modeName;
        return true;
    }

    public async ValueTask<bool> TrySelectNextModeAsync(CancellationToken ct = default) {
        string? collectionName = State.ThemeCollectionName;
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("No current theme collection.");

        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");

        string? currentMode = State.ThemeModeName;
        if (!collection.TryGetNextThemeMode(currentMode, out ThemeMode nextMode))
            return logger.WarningAsFalse("Could not get next theme mode.");

        string modeName = nextMode.Name;
         State.ThemeModeName = modeName;

        queryParameterManager.SetParams(
            (QueryParameterNames.ThemeCollection, State.ThemeCollectionName),
            (QueryParameterNames.ThemeMode, State.ThemeModeName)
        );

        await OnChangedAsyncInternal();
        return true;
    }

    private async ValueTask OnChangedAsyncInternal() {
        OnChanged?.Invoke();
        if (OnChangedAsync is not null) await OnChangedAsync();
    }

    public async ValueTask<IThemeCollection?> TryGetCollectionAsync(string themeName, CancellationToken ct = default) {
        if (themingConfig.RegisteredBaseThemes.TryGetValue(themeName, out IThemeCollection? theme)) {
            State.ThemeCollectionName = theme.CollectionName;
            await OnChangedAsyncInternal();
            return theme;
        }

        if (externalProvider is null) return null;

        return await externalProvider.TryGetCollectionAsync(themeName, ct);
    }

}
