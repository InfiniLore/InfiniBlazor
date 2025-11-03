// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IThemeProvider {
    event Func<string, string, Task>? OnThemeChangeRequestAsync;
    ThemeEntryData? LastKnownThemeEntryData { get; }
    (string CollectionName, string ThemeName) LastKnownThemeSelection { get; }
    
    Task<ThemeEntryData?> GetInitialThemeEntyData(CancellationToken ct = default);
    Task<ThemeEntryData?> GetThemeEntryData(string collectionName, string entryName, CancellationToken ct = default);
    Task<ThemeCollectionData?> GetThemeCollectionData(string collectionName, CancellationToken ct = default);

    Task InvokeThemeChangeRequestedAsync(string collectionName, string entryName);
}
