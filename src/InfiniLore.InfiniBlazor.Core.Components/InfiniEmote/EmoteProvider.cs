// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class EmoteProvider(ILogger<EmoteProvider> logger) : IEmoteProvider {
    private ConcurrentDictionary<string, IEmoteEntry> Entries { get; } = new(StringComparer.OrdinalIgnoreCase);

    private static JsonSerializerOptions JsonSerializerOptions { get; } = new() {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public int Count => Entries.Count;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetEntry(string key, [NotNullWhen(true)] out IEmoteEntry? entry) {
        entry = null;
        return !key.IsNullOrWhiteSpace()
            && Entries.TryGetValue(key, out entry);
    }
    
    public bool TryImportData(Stream fileStream) {
        try {
            int originalCount = Entries.Count;
            var enumerable = JsonSerializer.Deserialize<IEnumerable<EmoteEntry>>(fileStream, JsonSerializerOptions);
            if (enumerable is null) return false;
            
            int count = 0;
            foreach (EmoteEntry entry in enumerable) {
                count++;
                foreach (string key in entry.Keys) {
                    Entries.AddOrUpdate(
                        key,
                        _ => entry,
                        (_, _) => entry
                    );
                }
            }

            int newCount = Entries.Count;
            int offsetCount = newCount - originalCount;
            if (offsetCount <= 0) {
                logger.Warning("No new emote entries imported");
                return false;
            }

            logger.Information("Imported {emoteCount} emote entries, with a total of {keyCount}", count, offsetCount);
            return true;
            
        }
        catch (Exception ex) {
            logger.Error(ex, "Error importing emote entries");
            return false;
        }
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

            logger.Information("Imported {emoteCount} emote entries, with a total of {keyCount}", count, offsetCount);
            return true;
        }
        catch (Exception ex) when (ex is not TaskCanceledException) {
            logger.Error(ex, "Error importing emote entries");
            return false;
        }
    }

    public async Task<bool> TryWriteDataAsync(StreamWriter streamWriter, CancellationToken ct = default) {
        try {
            string jsonContent = JsonSerializer.Serialize(Entries, JsonSerializerOptions);

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
