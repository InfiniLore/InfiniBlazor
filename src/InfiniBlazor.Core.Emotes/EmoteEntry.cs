// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace InfiniBlazor.Emotes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record EmoteEntry(
    [property: JsonPropertyName("keys")] string[] Keys,
    [property: JsonPropertyName("data")] string? Data,
    [property: JsonPropertyName("contentType"), JsonConverter(typeof(JsonStringEnumConverter))] EmoteContentType ContentType
) : IEmoteEntry;
