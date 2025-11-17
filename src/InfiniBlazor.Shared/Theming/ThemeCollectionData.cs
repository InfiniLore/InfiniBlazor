// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.Json.Serialization;

namespace InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ThemeCollectionData(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("metadata")] ThemeCollectionMetadata Metadata,
    [property: JsonPropertyName("entries")] ThemeEntryData[] Entries
) {
    public bool IsBinaryTheme { get; } = Entries.Length == 2;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ThemeCollectionData AddEntry(ThemeEntryData entry) => this with { Entries = Entries.Append(entry).ToArray()};
}

// ---------------------------------------------------------------------------------------------------------------------
// Support Classes
// ---------------------------------------------------------------------------------------------------------------------
public record ThemeCollectionMetadata(
    [property: JsonPropertyName("author")] string Author,
    [property: JsonPropertyName("version")] string Version,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("primary_entry_name")] string? PrimaryEntryName
);
    
public record ThemeEntryData(
    [property: JsonPropertyName("name")] string EntryName,
    [property: JsonPropertyName("css")] string CssData
);