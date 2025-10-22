// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Components;
using System.Collections.Immutable;
using System.Reflection;

namespace InfiniLore.InfiniBlazor.Emotes.DataLoaders;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IEmoteDataLoader>(KeyName)]
public class EmbeddedResourceEmoteDataLoader(
    IEmotesConfig emotesConfig
) : IEmoteDataLoader {
    public const string KeyName = nameof(EmbeddedResourceEmoteDataLoader);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IEnumerable<Stream> LoadEmoteStreams() {
        if (!emotesConfig.TryGetEmbeddedResourceAssemblies(out ImmutableArray<Assembly> assemblies)) return Enumerable.Empty<Stream>();
        
        // TODO this is hacky and should be replaced by a proper solution
        HashSet<string> resourceFilePaths = emotesConfig.GetEmoteJsonLibFilePaths()
            .Select(static path => path.TrimStart('/')
                .Replace("_content/InfiniLore.InfiniBlazor.Emotes/", "InfiniLore.InfiniBlazor.Emotes.wwwroot.")
                .Replace("/", ".")
            ).ToHashSet();

        return resourceFilePaths.SelectMany(resourceName => assemblies
            .Select(assembly => assembly.GetManifestResourceStream(resourceName))
            .OfType<Stream>());
    }
}
