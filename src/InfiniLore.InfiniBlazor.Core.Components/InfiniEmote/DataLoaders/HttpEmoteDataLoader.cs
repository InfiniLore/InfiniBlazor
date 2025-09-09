// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Components.DataLoaders;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IEmoteDataLoader>]
public class HttpEmoteDataLoader(
    IHttpClientFactory clientFactory,
    IComponentsConfig componentsConfig, 
    ILogger<HttpEmoteDataLoader> logger
) : IEmoteDataLoader {
    public bool EnforceAsyncUsage => true;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async IAsyncEnumerable<Stream> LoadEmoteStreamsAsync(CancellationToken ct = default) {
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
    
    public IEnumerable<Stream> LoadEmoteStreams() {
        throw new NotSupportedException();
    }
}
