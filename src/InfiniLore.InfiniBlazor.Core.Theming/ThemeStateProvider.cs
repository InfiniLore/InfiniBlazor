// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;

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
    private readonly ThemeState State = GetInitialState(themingConfig);
    
    private FrozenDictionary<string, IThemeCollection> RegisteredThemeCollections => themingConfig.GetRegisteredThemeCollections();

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static ThemeState GetInitialState(IThemingConfig themingConfig) {
        string collectionName = themingConfig.DefaultThemeCollectionName;
        string modeName = themingConfig.DefaultThemeMode.Name;

        themingConfig.GetRegisteredThemeCollections().TryGetValue(collectionName, out IThemeCollection? theme);

        return new ThemeState {
            CollectionName = collectionName,
            ModeName = modeName,
            IsBinaryCollection = theme?.IsBinary ?? false,
            FirstModeInCollectionName = theme?.AllModeNames.FirstOrDefault()
        };
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IThemeState GetState()
        => State;

    public async ValueTask InitializeFromQueryAsync(CancellationToken ct = default) {
        string? collectionName = queryParameterManager.GetParam<string>(QueryParameterNames.ThemeCollection);
        string? modeName = queryParameterManager.GetParam<string>(QueryParameterNames.ThemeMode);

        if (!collectionName.IsNullOrWhiteSpace())
            await TrySelectCollectionInternalAsync(collectionName, ct, true);

        if (!modeName.IsNullOrWhiteSpace())
            await TrySelectModeInternalAsync(modeName, ct, true);
    }

    public async ValueTask<bool> TrySelectCollectionAsync(string collectionName, CancellationToken ct = default) {
        if (!await TrySelectCollectionInternalAsync(collectionName, ct, false)) return false;

        await UpdateQueryParamsAndNotify();
        return true;
    }

    public async ValueTask<bool> TrySelectModeAsync(string modeName, CancellationToken ct = default) {
        if (!await TrySelectModeInternalAsync(modeName, ct, false)) return false;

        await UpdateQueryParamsAndNotify();
        return true;
    }

    public async ValueTask<bool> TrySelectNextModeAsync(CancellationToken ct = default) {
        string? collectionName = State.CollectionName;
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("No current theme collection.");

        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");

        string? currentMode = State.ModeName;
        if (!collection.TryGetNextThemeMode(currentMode, out ThemeMode nextMode))
            return logger.WarningAsFalse("Could not get next theme mode.");

        await UpdateState(collectionName, nextMode.Name, ct);

        await UpdateQueryParamsAndNotify();
        return true;
    }

    private async ValueTask<bool> TrySelectCollectionInternalAsync(string collectionName, CancellationToken ct, bool updateStateOnly) {
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Theme name is null or empty.");

        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");

        string? modeName = State.ModeName;
        if (modeName.IsNullOrWhiteSpace() || !collection.ContainsModeName(modeName))
            modeName = collection.GetFirstMode().Name;

        await UpdateState(collection.CollectionName, modeName, ct);

        if (!updateStateOnly) await OnChangedAsyncInternal();

        return true;
    }

    private async ValueTask<bool> TrySelectModeInternalAsync(string modeName, CancellationToken ct, bool updateStateOnly) {
        if (modeName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Mode name is null or empty.");

        string? collectionName = State.CollectionName;
        if (collectionName.IsNullOrWhiteSpace()) return logger.WarningAsFalse("Could not find theme collection.");

        IThemeCollection? collection = await TryGetCollectionAsync(collectionName, ct);
        if (collection is null) return logger.WarningAsFalse("Could not find theme collection.");

        if (!collection.ContainsModeName(modeName))
            modeName = collection.GetFirstMode().Name;

        await UpdateState(collectionName, modeName, ct);

        if (!updateStateOnly) await OnChangedAsyncInternal();

        return true;
    }

    private async ValueTask UpdateState(string? collectionName, string? modeName, CancellationToken ct) {
        if (collectionName.IsNullOrWhiteSpace()) return;

        if (!RegisteredThemeCollections.TryGetValue(collectionName, out IThemeCollection? collection)) {
            if (externalProvider is not null) {
                collection = await externalProvider.TryGetCollectionAsync(collectionName, ct);
            }
        }

        if (collection is not null) {
            State.CollectionName = collection.CollectionName;
            State.IsBinaryCollection = collection.IsBinary;
            State.FirstModeInCollectionName = collection.AllModeNames.FirstOrDefault();
        }
        else {
            // fallback: just assign collectionName if collection can't be found
            State.CollectionName = collectionName;
            State.IsBinaryCollection = false;
            State.FirstModeInCollectionName = null;
        }

        if (!modeName.IsNullOrWhiteSpace()) {
            State.ModeName = modeName;
        }
    }

    private async Task UpdateQueryParamsAndNotify() {
        queryParameterManager.SetParams(
            (QueryParameterNames.ThemeCollection, State.CollectionName),
            (QueryParameterNames.ThemeMode, State.ModeName)
        );

        await OnChangedAsyncInternal().ConfigureAwait(false);
    }

    private async ValueTask OnChangedAsyncInternal() {
        OnChanged?.Invoke();
        if (OnChangedAsync is not null) await OnChangedAsync();
    }

    public async ValueTask<IThemeCollection?> TryGetCollectionAsync(string themeName, CancellationToken ct = default) {
        if (RegisteredThemeCollections.TryGetValue(themeName, out IThemeCollection? theme)) {
            return theme;
        }

        if (externalProvider is null) return null;

        return await externalProvider.TryGetCollectionAsync(themeName, ct);
    }

}
