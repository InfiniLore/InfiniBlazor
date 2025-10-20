// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace DevTools.Blazor.Components.Pages.EmoteDataEditor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class GistEmoteJson {
    [JsonPropertyName("emojis")] public GistEmoteJsonEntry[] Emotes { get; set; } = [];
}

[UsedImplicitly]
public class GistEmoteJsonEntry {
    [JsonPropertyName("emoji")] public string? Emoji { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("shortname")] public string? ShortName { get; set; }
    [JsonPropertyName("unicode")] public string? Unicode { get; set; }
    [JsonPropertyName("html")] public string? Html { get; set; }
    [JsonPropertyName("category")] public string? Category { get; set; }
    [JsonPropertyName("order")] public string? Order { get; set; }
    
    public static string ToUnicodeEscapedString(string hexSequence) {
        var sb = new StringBuilder();
        ReadOnlySpan<char> hexSequenceSpan = hexSequence.AsSpan();
        foreach (Range hexValue in hexSequenceSpan.Split(' ')) {
            ReadOnlySpan<char> token = hexSequenceSpan[hexValue].Trim();
            if (token.IsEmpty) continue;
            if (!int.TryParse(token, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int value)) continue;
            
            sb.Append($"\\u{value:X4}");
        }
        
        return sb.ToString();
    }
}

public class GistEmoteJsonEntryDto {
    public required string ShortName { get; init; }
    public required string Emoji { get; init; }
    public required string Unicode { get; init; }
    
    public static bool TryParseFromJsonEntry(GistEmoteJsonEntry entry, [NotNullWhen(true)] out GistEmoteJsonEntryDto? dto) {
        dto = null;
        if (entry.Emoji.IsNullOrWhiteSpace() || entry.Unicode.IsNullOrWhiteSpace() || entry.ShortName.IsNullOrWhiteSpace()) return false;
        dto = new GistEmoteJsonEntryDto {
            ShortName = entry.ShortName,
            Emoji = entry.Emoji,
            Unicode = GistEmoteJsonEntry.ToUnicodeEscapedString(entry.Unicode)
        };
        return true;
    }
}
