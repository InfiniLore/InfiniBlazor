// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniLore.InfiniBlazor.Emotes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IEmoteProvider>]
public class EmoteProvider(ILogger<EmoteProvider> logger) : IEmoteProvider {
    private ConcurrentDictionary<string, EmoteEntry> Entries { get; set; } = new();

    private static JsonSerializerOptions JsonSerializerOptions { get; } = new() {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };
    
    public int Count => Entries.Count;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async Task InitializeAsync(CancellationToken ct = default) {
        Assembly assembly = typeof(EmoteProvider).Assembly;
        string[] resourceNames = {
            "InfiniLore.InfiniBlazor.wwwroot.libs.emotes.emotes_standard.json",
            "InfiniLore.InfiniBlazor.wwwroot.libs.emotes.emotes_lucide.json"
        };
        
        List<Stream> streams = resourceNames
            .Select(resourceName => assembly.GetManifestResourceStream(resourceName))
            .Where(stream => stream is not null)!
            .ToList<Stream>();

        await Task.WhenAll(streams.Select(stream => TryImportDataAsync(stream, ct)));
        
        foreach (Stream stream in streams) {
            await stream.DisposeAsync();
        }
    }
    
    public bool HasKey(string key) => Entries.ContainsKey(key);
    public bool HasKey(ReadOnlySpan<char> key) => Entries.TryGetAlternateLookup(out ConcurrentDictionary<string, EmoteEntry>.AlternateLookup<ReadOnlySpan<char>> lookup) && lookup.ContainsKey(key);
    
    public bool TryGetEntry(string? key, [NotNullWhen(true)] out IEmoteEntry? entry) {
        entry = null;
        if (key is null) return false;
        if (!Entries.TryGetValue(key, out EmoteEntry? value)) return false;

        entry = value;
        return true;
    }
    
    public bool TryGetEntry(ReadOnlySpan<char> key, [NotNullWhen(true)] out IEmoteEntry? entry) {
        entry = null;
        if (!Entries.TryGetAlternateLookup(out ConcurrentDictionary<string, EmoteEntry>.AlternateLookup<ReadOnlySpan<char>> lookup)) return false;
        if (!lookup.TryGetValue(key, out EmoteEntry? value)) return false;

        entry = value;
        return true;
    }

    public async Task<bool> TryImportDataAsync(Stream fileStream, CancellationToken ct = default) {
        try {
            int originalCount = Entries.Count;

            IAsyncEnumerable<EmoteEntry?> enumerable = JsonSerializer.DeserializeAsyncEnumerable<EmoteEntry>(fileStream, JsonSerializerOptions, ct);

            int count = 0;
            await foreach (EmoteEntry? emoteEntry in enumerable) {
                if (emoteEntry is null) continue;

                count++;
                foreach (string key in emoteEntry.Keys) {
                    Entries.AddOrUpdate(
                        key,
                        _ => emoteEntry,
                        (_, _) => emoteEntry
                    );
                }
            }
            
            int newCount = Entries.Count;
            int offsetCount = newCount - originalCount;
            if (offsetCount <= 0) {
                logger.Warning("No new emote entries imported");
                return false;           
            }

            logger.Information("Imported {emoteCount} emote entries, with a total of {keyCount}",count, offsetCount);
            return true;
        }
        catch (Exception ex) when (ex is not TaskCanceledException) {
            logger.Error(ex, "Error importing emote entries");
            return false;
        }
    }

    public async Task<bool> TryWriteDataAsync(StreamWriter streamWriter, CancellationToken ct = default) {
        try {
            string jsonContent = JsonSerializer.Serialize(Entries,JsonSerializerOptions);

            await streamWriter.WriteAsync(jsonContent.AsMemory(), ct);
            await streamWriter.FlushAsync(ct);

            logger.Information("Wrote {count} emote entries", Entries.Count);

            return true;
        }
        catch (Exception ex) when (ex is not TaskCanceledException) {
            logger.Error(ex, "Error writing emote entries");
            return false;
        }
    }

}
