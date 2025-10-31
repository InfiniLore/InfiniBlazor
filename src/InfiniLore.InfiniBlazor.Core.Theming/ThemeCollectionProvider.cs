// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;

namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IThemeCollectionProvider>]
public class ThemeCollectionProvider(
    IThemingConfig config,
    // HttpClient httpClient,
    ILogger<ThemeCollectionProvider> logger
) : IThemeCollectionProvider {
    private ConcurrentDictionary<string, ThemeCollectionData> CachedThemeCollections { get; } = new();
    private bool _isInitialized;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private async ValueTask InitializeIfNeededAsync(CancellationToken ct = default) {
        if (_isInitialized) return;

        ThemeResource[] knownResources = config.GetThemeResources();

        Task<bool>[] tasks = knownResources.Select(resource => LoadThemeResourceAsync(resource, ct)).ToArray();
        bool[] results = await Task.WhenAll(tasks);
        if (!results.All(r => r) && config.ThrowOnBrokenInitialization) {
            logger.LogWarning("Could not load all theme resources.");
            throw new Exception("Could not load all theme resources.");   
        }
        
        _isInitialized = true;
    }

    private async Task<bool> LoadThemeResourceAsync(ThemeResource resource, CancellationToken ct = default) {
        switch (resource.Location) {
            case ThemeResourceLocation.EmbeddedResource: {
                try {
                    Assembly assembly = typeof(ThemeCollectionProvider).Assembly;
                    string resourceName = resource.Path;
                    
                    await using Stream? stream = assembly.GetManifestResourceStream(resourceName);
                    if (stream == null) {
                        logger.LogWarning("Could not find embedded resource '{ResourceName}' in the assembly {assembly}.", resourceName, assembly.FullName);
                        return false;   
                    }

                    using var reader = new StreamReader(stream);
                    string json = await reader.ReadToEndAsync(ct);
                    if (JsonSerializer.Deserialize<ThemeCollectionData>(json) is not {} collectionData) {
                        logger.LogWarning("Could not deserialize theme collection data from embedded resource '{ResourceName}' in the assembly {assembly}.", resourceName, assembly.FullName);
                        return false;   
                    }

                    // ReSharper disable once InvertIf
                    if (!CachedThemeCollections.TryAdd(collectionData.Name, collectionData)) {
                        logger.LogWarning("Could not add theme collection {name} data to cache.", collectionData.Name);
                        return false;  
                    }
                    
                    return true;
                }
                catch (Exception e) {
                    logger.LogWarning(e, "Error trying to load theme collection from embedded resource '{ResourceName}' in the assembly {assembly}.", resource.Path, typeof(ThemeCollectionProvider).Assembly.FullName);
                    return false; 
                }
            }

            case ThemeResourceLocation.LocalFilePath:
            case ThemeResourceLocation.WebUrl: {
                throw new NotImplementedException();
            }

            default: {
                logger.LogWarning("Unsupported theme resource location: {location}", resource.Location);
                return false;
            }
        }
    }

    public async ValueTask<ThemeCollectionData?> GetThemeCollectionAsync(string collectionName, CancellationToken ct = default) {
        await InitializeIfNeededAsync(ct);
        return CachedThemeCollections.GetValueOrDefault(collectionName);   
    }
}
