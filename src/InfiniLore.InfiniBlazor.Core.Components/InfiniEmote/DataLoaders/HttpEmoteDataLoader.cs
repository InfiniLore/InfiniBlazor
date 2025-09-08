// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Components.DataLoaders;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IEmoteDataLoader>]
public class HttpEmoteDataLoader(
    [FromKeyedServices(HttpClientNames.InfiniBlazor)] HttpClient client,
    IComponentsConfig componentsConfig, 
    ILogger<HttpEmoteDataLoader> logger
) : IEmoteDataLoader {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task<Stream[]> LoadEmoteStreamsAsync(CancellationToken ct = default) {
        ImmutableArray<string> resourceFilepaths = componentsConfig.GetEmoteJsonLibFilePaths();
        if (resourceFilepaths.IsEmpty) return [];
        
        Stream[] streams = await Task.WhenAll(
            resourceFilepaths.Select(async resourcePath => {
                try {
                    // ReSharper disable once AccessToDisposedClosure
                    return await client.GetStreamAsync(resourcePath, ct);
                }
                catch (Exception ex) {
                    logger.Warning(ex, "Failed to download emote resource at {resourcePath}", resourcePath);
                    return null;
                }
            })
        ).ContinueWith<Stream[]>(
            static t => t.Result.Where(s => s != null).ToArray()!,
            ct
        );

        return streams;
    }
}
