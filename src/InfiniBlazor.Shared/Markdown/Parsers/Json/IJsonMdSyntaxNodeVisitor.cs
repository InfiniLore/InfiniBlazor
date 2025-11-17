// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Text.Json;

namespace InfiniBlazor.Markdown.Parsers.Json;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJsonMdSyntaxNodeVisitor {
    void DeserializeToJson(IMdSyntaxNode node, Utf8JsonWriter writer);
    IMdSyntaxNode SerializeToNode(JsonElement element, IMdSyntaxNode parentNode);
}
