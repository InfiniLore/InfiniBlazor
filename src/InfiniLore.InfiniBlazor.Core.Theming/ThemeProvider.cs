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
    IThemeCollectionProvider collectionProvider,
    IThemingConfig config,
    ILogger<ThemeProvider> logger
) : IThemeProvider {
    public event Func<string, string,Task>? OnThemeChangeRequestAsync;
    public ThemeEntryData? LastKnownThemeEntryData { get; private set; }
    public (string CollectionName, string ThemeName) LastKnownThemeSelection { get; private set; } = config.DefaultThemeSelection;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task<ThemeEntryData?> GetInitialThemeEntyData(CancellationToken ct = default) {
        (string collectionName, string entryName) = config.DefaultThemeSelection;
        ThemeEntryData? data = await GetThemeEntryData(collectionName, entryName, ct);
        LastKnownThemeEntryData = data;
        return data;   
    }

    public async Task<ThemeEntryData?> GetThemeEntryData(string collectionName, string entryName, CancellationToken ct = default) {
        if (await collectionProvider.GetThemeCollectionAsync(collectionName, ct) is not {} collection) {
            logger.Warning("Could not find default theme collection of name {Name}. This is usually due to a configuration issue.", collectionName);
            return null;
        }

        // ReSharper disable once InvertIf
        if (collection.Entries.FirstOrDefault(e => e.EntryName == entryName) is not {} entry) {
            logger.Warning("Could not find default theme entry of name {EntryName} within {CollectionName}. This is usually due to a configuration issue.", entryName, collectionName);
            return null;
        }
        
        return entry;   
    }
    
    public async Task<ThemeCollectionData?> GetThemeCollectionData(string collectionName, CancellationToken ct = default) {
        // ReSharper disable once InvertIf
        if (await collectionProvider.GetThemeCollectionAsync(collectionName, ct) is not {} collection) {
            logger.Warning("Could not find default theme collection of name {Name}. This is usually due to a configuration issue.", collectionName);
            return null;
        }
        
        return collection;  
    }

    public async Task InvokeThemeChangeRequestedAsync(string collectionName, string entryName) {
        if (OnThemeChangeRequestAsync is null) return;
        LastKnownThemeEntryData = await GetThemeEntryData(collectionName, entryName);
        LastKnownThemeSelection = (collectionName, entryName);
        await OnThemeChangeRequestAsync.Invoke(collectionName, entryName);
    }
}
