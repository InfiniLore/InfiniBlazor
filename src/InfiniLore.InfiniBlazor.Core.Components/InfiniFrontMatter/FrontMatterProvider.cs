// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Components.FrontMatter;
using InfiniLore.Lucide;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using YamlDotNet.Serialization;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IFrontMatterProvider>]
public class FrontMatterProvider(ILogger<FrontMatterProvider> logger) : IFrontMatterProvider {
    private static readonly IDeserializer YamlDeserializer = new DeserializerBuilder().Build();

    private FrozenDictionary<string, string> KeyIconMap { get; } = new Dictionary<string, string> {
        ["key"] = LucideNames.Lock
    }.ToFrozenDictionary();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetIconName(string key, [NotNullWhen(true)] out string? icon)
        => KeyIconMap.TryGetValue(key, out icon);

    public bool TryParse(string? value, string? lang, [NotNullWhen(true)] out IEnumerable<IFrontMatterEntry>? entries) {
        logger.Information("Parsing front matter {value}, {lang}", value, lang);
        entries = null;
        if (value.IsNullOrWhiteSpace()) return false;

        logger.Information("Parsing front matter");

        try {
            switch (lang) {
                case null or "" or "yaml" or "yml": {
                    logger.Information("Parsing YAML front matter");
                    var data = YamlDeserializer.Deserialize<Dictionary<string, string?>>(value);
                    entries = data.Select(kvp => new FrontMatterEntryData(kvp.Key, kvp.Value));
                    return !entries.IsEmpty();
                }

                case "json": {
                    logger.Information("Parsing JSON front matter");
                    if (!value.StartsWith('{') && !value.EndsWith('}')) value = $"{{{value}}}";
                    var data = JsonSerializer.Deserialize<Dictionary<string, string?>>(value);
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
}
