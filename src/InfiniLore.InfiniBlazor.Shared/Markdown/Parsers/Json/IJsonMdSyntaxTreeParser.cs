// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.Json;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Json;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJsonMdSyntaxTreeParser {
    string DeserializeToString(IMdSyntaxTree input);
    JsonElement DeserializeToJsonElement(IMdSyntaxTree tree);
    Task DeserializeToJsonStreamAsync(Stream stream, IMdSyntaxTree tree, CancellationToken ct = default);
    Task DeserializeToJsonFileAsync(string filePath, IMdSyntaxTree tree, CancellationToken ct = default);
    
    IMdSyntaxTree SerializeToSyntaxTree(JsonElement element);
    Task<IMdSyntaxTree> SerializeToSyntaxTreeAsync(Stream stream, CancellationToken ct = default);
    Task<IMdSyntaxTree> SerializeToSyntaxTreeAsync(string filePath, CancellationToken ct = default);
}
