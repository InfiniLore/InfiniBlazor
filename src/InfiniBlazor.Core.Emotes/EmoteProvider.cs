// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniBlazor.Emotes.DataLoaders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniBlazor.Emotes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IEmoteProvider>]
public class EmoteProvider(
    ILogger<EmoteProvider> logger, 
    [FromKeyedServices(EmbeddedResourceEmoteDataLoader.KeyName)] IEmoteDataLoader dataLoader
    ) : IEmoteProvider {
    private FrozenDictionary<string, IEmoteEntry> Entries { get; } = GetEntries(dataLoader, logger);

    private static JsonSerializerOptions JsonSerializerOptions { get; } = new() {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public int Count => Entries.Count;

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, IEmoteEntry> GetEntries(IEmoteDataLoader dataLoader, ILogger<EmoteProvider> logger) {
        var dictionary = new Dictionary<string, IEmoteEntry>(4000, StringComparer.OrdinalIgnoreCase);
        
        foreach (Stream stream in dataLoader.LoadEmoteStreams()) {
            IEnumerable<EmoteEntry>? enumerable;
            try {
                enumerable = JsonSerializer.Deserialize<IEnumerable<EmoteEntry>>(stream, JsonSerializerOptions);
            }
            catch (Exception e) {
                logger.Error(e, "Error loading emote data from stream");
                continue;
            }
            if (enumerable is null) continue;
            foreach (EmoteEntry entry in enumerable) {
                foreach (string key in entry.Keys) {
                    dictionary.AddOrUpdate(
                        key,
                        _ => entry,
                        (_, _) => entry
                    );
                }
            }
        }

        return dictionary.ToFrozenDictionary();
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetEntry(string key, [NotNullWhen(true)] out IEmoteEntry? entry) {
        entry = null;
        return !key.IsNullOrWhiteSpace() && Entries.TryGetValue(key, out entry);
    }
}
