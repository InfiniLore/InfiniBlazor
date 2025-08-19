// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeJsonParser {
    private static readonly JsonSerializerOptions SerializerOptions = new() {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = true,
        Converters = {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            new MdSyntaxNodeJsonConverter() // Custom converter for polymorphic serialization
        }
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Async Serialization
    // -----------------------------------------------------------------------------------------------------------------
    public async Task ToJsonAsync(Stream stream, IMdSyntaxNode node, CancellationToken ct = default) {
        ArgumentNullException.ThrowIfNull(stream);
        ArgumentNullException.ThrowIfNull(node);

        await JsonSerializer.SerializeAsync(stream, node, node.GetType(), SerializerOptions, ct);
        await stream.FlushAsync(ct);
    }

    public async Task ToJsonFileAsync(string filePath, IMdSyntaxNode node, CancellationToken ct = default) {
        if (filePath.IsNullOrWhiteSpace()) throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));
        ArgumentNullException.ThrowIfNull(node);

        await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
        await ToJsonAsync(fileStream, node, ct);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Async Deserialization
    // -----------------------------------------------------------------------------------------------------------------
    public async Task<T> FromJsonAsync<T>(Stream stream, CancellationToken ct = default) where T : IMdSyntaxNode {
        ArgumentNullException.ThrowIfNull(stream);

        var node = await JsonSerializer.DeserializeAsync<T>(stream, SerializerOptions, ct);
        if (node == null) throw new InvalidOperationException("Deserialized node is null.");

        return node;
    }

    public async Task<T> FromJsonFileAsync<T>(string filePath, CancellationToken ct = default) where T : IMdSyntaxNode {
        if (filePath.IsNullOrWhiteSpace()) throw new ArgumentException("File path cannot be null or whitespace.", nameof(filePath));

        await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        return await FromJsonAsync<T>(fileStream, ct);
    }
}