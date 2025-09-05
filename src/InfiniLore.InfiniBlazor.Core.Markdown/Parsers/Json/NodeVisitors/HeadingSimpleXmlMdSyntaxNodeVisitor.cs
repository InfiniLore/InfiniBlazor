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
public sealed class HeadingSimpleJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<HeadingSimpleMdSyntaxNode> {
    private static readonly string Identifier = nameof(HeadingSimpleMdSyntaxNode.Identifier).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(HeadingSimpleMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteString(Identifier, node.Identifier);
    }

    protected override void SerializeDetails(JsonElement element, HeadingSimpleMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(Identifier, out JsonElement identifierProperty)) {
            targetNode.WithIdentifier(identifierProperty.GetString() ?? string.Empty);
        }
    }

}
