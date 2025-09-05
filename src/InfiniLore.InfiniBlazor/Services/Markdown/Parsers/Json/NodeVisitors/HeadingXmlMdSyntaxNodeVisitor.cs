// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.Json;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Json.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HeadingJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<HeadingMdSyntaxNode> {
    private static readonly string Level = nameof(HeadingMdSyntaxNode.Level).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(HeadingMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteNumber(Level, node.Level);
    }

    protected override void SerializeDetails(JsonElement element, HeadingMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(Level, out JsonElement levelProperty)) {
            targetNode.WithLevel(levelProperty.GetInt32());
        }
    }
}
