// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.Lucide;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IFrontMatterProvider>]
public class FrontMatterProvider(ILogger<FrontMatterProvider> logger) : IFrontMatterProvider {
    private static readonly IDeserializer YamlDeserializer = new DeserializerBuilder().Build();
    private static readonly ISerializer YamlSerializer = new SerializerBuilder().Build();

    private FrozenDictionary<string, string> KeyIconMap { get; } = new Dictionary<string, string> {
        ["key"] = LucideNames.Lock
    }.ToFrozenDictionary();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetIconName(string key, [NotNullWhen(true)] out string? icon)
        => KeyIconMap.TryGetValue(key, out icon);

    public bool TryParse(string? value, string? lang, [NotNullWhen(true)] out IEnumerable<IFrontMatterEntry>? entries) {
        entries = null;
        if (value.IsNullOrWhiteSpace()) return false;
        
        try {
            switch (lang) {
                case null or "" or "yaml" or "yml": {
                    var data = YamlDeserializer.Deserialize<Dictionary<string, object?>>(value);
                    entries = data.Select(kvp => new FrontMatterEntryData(kvp.Key, kvp.Value));
                    return !entries.IsEmpty();
                }

                case "json": {
                    if (!value.StartsWith('{') && !value.EndsWith('}')) value = $"{{{value}}}";
                    var data = JsonSerializer.Deserialize<Dictionary<string, object?>>(value);
                    entries = data?.Select(kvp => new FrontMatterEntryData(kvp.Key, kvp.Value));
                    return !entries?.IsEmpty() ?? false;
                }

                default:
                    logger.Warning("Unsupported front matter language: {lang}", lang);
                    return false;
            }

        }
        catch (Exception ex) {
            logger.Warning(ex, "Error parsing front matter");
            entries = null;// Just to ensure we don't return a value that has been wrongly set
            return false;
        }
    }
    public bool TryParse(IEnumerable<IFrontMatterEntry> entries, string? lang, [NotNullWhen(true)] out string? value) {
        value = null;
        
        try {
            switch (lang) {
                case null or "" or "yaml" or "yml": {
                    Dictionary<string, object?> dict = entries.ToDictionary(entry => entry.Key, entry => entry.Value);
                    value = YamlSerializer.Serialize(dict);
                    return value.IsNotNullOrWhiteSpace();
                }

                case "json": {
                    Dictionary<string, object?> dict = entries.ToDictionary(entry => entry.Key, entry => entry.Value);
                    value = JsonSerializer.Serialize(dict);
                    return value.IsNotNullOrWhiteSpace();
                }

                default:
                    logger.Warning("Unsupported front matter language: {lang}", lang);
                    return false;
            }

        }
        catch (Exception ex) {
            logger.Warning(ex, "Error parsing front matter");
            value = null;// Just to ensure we don't return a value that has been wrongly set
            return false;
        }
    }
}
