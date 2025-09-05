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
public sealed class HtmlSpanJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<HtmlSpanMdSyntaxNode> {
    private static readonly string Attributes = nameof(HtmlSpanMdSyntaxNode.Attributes).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(HtmlSpanMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteString(Attributes, node.Attributes);
    }

    protected override void SerializeDetails(JsonElement element, HtmlSpanMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(Attributes, out JsonElement attributesProperty)) {
            targetNode.WithAttributes(attributesProperty.GetString() ?? string.Empty);
        }
    }

}
