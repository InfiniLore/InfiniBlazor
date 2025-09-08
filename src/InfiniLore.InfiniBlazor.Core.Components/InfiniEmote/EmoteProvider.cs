// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IEmoteProvider>]
public class EmoteProvider(ILogger<EmoteProvider> logger, [FromKeyedServices("EmbeddedResource")] IEmoteDataLoader dataLoader) : IEmoteProvider {
    private ConcurrentDictionary<string, EmoteEntry> Entries { get; } = new();

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
        Stream[] streams = await dataLoader.LoadEmoteStreamsAsync(ct).ConfigureAwait(false);

        if (streams.Length == 0) {
            logger.Error("Could not load any emote JSON resources");
            return;
        }

        await Task.WhenAll(streams.Select(stream => TryImportDataAsync(stream, ct))).ConfigureAwait(false);

        foreach (Stream stream in streams) {
            await stream.DisposeAsync().ConfigureAwait(false);
        }
    }
    
    public IEmoteEntry? GetEntryAsync(string key) {
        if (key.IsNullOrWhiteSpace()) return null;
        if (Entries.IsEmpty) _ = Task.Run(() => InitializeAsync()) ;
        Span<char> lowered = stackalloc char[key.Length];
        key.AsSpan().ToLowerInvariant(lowered);
        if (!Entries.TryGetAlternateLookup(out ConcurrentDictionary<string, EmoteEntry>.AlternateLookup<ReadOnlySpan<char>> lookup)) return null;
        return !lookup.TryGetValue(lowered, out EmoteEntry? value) ? null : value;
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
