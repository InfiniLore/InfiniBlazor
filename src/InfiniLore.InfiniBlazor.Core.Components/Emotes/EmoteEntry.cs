// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace InfiniLore.InfiniBlazor.Components.Emotes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record EmoteEntry(
    [property: JsonPropertyName("keys")] ICollection<string> Keys,
    [property: JsonPropertyName("data")] string? Data,
    [property: JsonPropertyName("contentType")] EmoteContentType ContentType
) : IEmoteEntry;

