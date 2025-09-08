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
[InjectableSingleton<IEmoteDataLoader>("EmbeddedResource")]
public class EmbeddedResourceEmoteDataLoader(
    IComponentsConfig componentsConfig
) : IEmoteDataLoader {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Task<Stream[]> LoadEmoteStreamsAsync(CancellationToken ct = default) {
        if (!componentsConfig.TryGetEmbeddedResourceAssemblies(out ImmutableArray<Assembly> assemblies)) return Task.FromResult(Array.Empty<Stream>());
        
        HashSet<string> resourceFilePaths = componentsConfig.GetEmoteJsonLibFilePaths()
            .Select(static path => path.TrimStart('/')
            .Replace("_content/InfiniLore.InfiniBlazor/", "InfiniLore.InfiniBlazor.wwwroot.")
            .Replace("/", ".")
        ).ToHashSet();

        return Task.FromResult(resourceFilePaths.SelectMany(
                resourceName => assemblies
                    .Select(assembly => assembly.GetManifestResourceStream(resourceName))
                    .OfType<Stream>())
            .ToArray());
    }
}
