// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Collections.Immutable;
using System.Reflection;

namespace InfiniLore.InfiniBlazor.Components.DataLoaders;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IEmoteDataLoader>(KeyName)]
public class EmbeddedResourceEmoteDataLoader(
    IComponentsConfig componentsConfig
) : IEmoteDataLoader {
    public bool EnforceAsyncUsage => false;
    public const string KeyName = nameof(EmbeddedResourceEmoteDataLoader);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Task<Stream[]> LoadEmoteStreamsAsync(CancellationToken ct = default) {
        throw new NotSupportedException();
    }

    public IEnumerable<Stream> LoadEmoteStreams() {
        if (!componentsConfig.TryGetEmbeddedResourceAssemblies(out ImmutableArray<Assembly> assemblies)) return Enumerable.Empty<Stream>();
        
        HashSet<string> resourceFilePaths = componentsConfig.GetEmoteJsonLibFilePaths()
            .Select(static path => path.TrimStart('/')
                .Replace("_content/InfiniLore.InfiniBlazor/", "InfiniLore.InfiniBlazor.wwwroot.")
                .Replace("/", ".")
            ).ToHashSet();

        return resourceFilePaths.SelectMany(resourceName => assemblies
            .Select(assembly => assembly.GetManifestResourceStream(resourceName))
            .OfType<Stream>());
    }
}
