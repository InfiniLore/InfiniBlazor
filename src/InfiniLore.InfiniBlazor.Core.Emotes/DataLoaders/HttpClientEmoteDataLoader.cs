// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace InfiniLore.InfiniBlazor.Emotes.DataLoaders;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IEmoteDataLoaderAsync>]
public class HttpClientEmoteDataLoader(
    IHttpClientFactory clientFactory,
    IEmotesConfig emotesConfig, 
    ILogger<HttpClientEmoteDataLoader> logger
) : IEmoteDataLoaderAsync {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async IAsyncEnumerable<Stream> LoadEmoteStreamsAsync([EnumeratorCancellation] CancellationToken ct = default) {
        ImmutableArray<string> resourceFilePaths = emotesConfig.GetEmoteJsonLibFilePaths();
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
