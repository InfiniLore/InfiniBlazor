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
public sealed class ListUnOrderedJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<ListUnOrderedMdSyntaxNode> {
    private static readonly string LeadingSpaces = nameof(ListUnOrderedMdSyntaxNode.LeadingSpaces).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ListUnOrderedMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteNumber(LeadingSpaces, node.LeadingSpaces);
    }

    protected override void SerializeDetails(JsonElement element, ListUnOrderedMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(LeadingSpaces, out JsonElement leadingSpacesProperty)) {
            targetNode.WithLeadingSpaces(leadingSpacesProperty.GetInt32());
        }
    }
}
