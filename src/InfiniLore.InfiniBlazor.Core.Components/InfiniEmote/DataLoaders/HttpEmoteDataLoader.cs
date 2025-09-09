// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace InfiniLore.InfiniBlazor.Components.DataLoaders;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IEmoteDataLoaderAsync>]
public class HttpEmoteDataLoader(
    IHttpClientFactory clientFactory,
    IComponentsConfig componentsConfig, 
    ILogger<HttpEmoteDataLoader> logger
) : IEmoteDataLoaderAsync {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async IAsyncEnumerable<Stream> LoadEmoteStreamsAsync([EnumeratorCancellation] CancellationToken ct = default) {
        ImmutableArray<string> resourceFilePaths = componentsConfig.GetEmoteJsonLibFilePaths();
        if (resourceFilePaths.IsEmpty) yield break;
        
        using HttpClient client = clientFactory.CreateClient();
        foreach (string resourcePath in resourceFilePaths) {
            Stream? stream = null;
            try {
                stream = await client.GetStreamAsync(resourcePath, ct);
            }
            catch (Exception ex) {
                logger.Warning(ex, "Failed to download emote resource at {resourcePath}", resourcePath);
            }
            if (stream is not null) yield return stream;
        }
    }
}
