// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.Json;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Json.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListItemJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<ListItemMdSyntaxNode> {
    private const string LeadingSpaces = nameof(ListItemMdSyntaxNode.LeadingSpaces);
    private const string CheckLeadingSpaces = nameof(ListItemMdSyntaxNode.CheckLeadingSpaces);
    private const string Index = nameof(ListItemMdSyntaxNode.Index);
    private const string CheckMarker = nameof(CheckMarker);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ListItemMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteString(CheckMarker, node.OriginalCheckMarker);
        writer.WriteString(Index, node.Index);
        writer.WriteNumber(LeadingSpaces, node.LeadingSpaces);
        writer.WriteNumber(CheckLeadingSpaces, node.CheckLeadingSpaces);
    }

    protected override void SerializeDetails(JsonElement element, ListItemMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(CheckMarker, out JsonElement checkMarkerProperty)) {
            targetNode.WithCheckMarker(checkMarkerProperty.GetString() ?? string.Empty);
        }

        if (element.TryGetProperty(Index, out JsonElement indexProperty)) {
            targetNode.WithIndex(indexProperty.GetString() ?? string.Empty);
        }

        if (element.TryGetProperty(LeadingSpaces, out JsonElement leadingSpacesProperty)) {
            targetNode.WithLeadingSpaces(leadingSpacesProperty.GetInt32());
        }

        if (element.TryGetProperty(CheckLeadingSpaces, out JsonElement checkLeadingSpacesProperty)) {
            targetNode.WithCheckLeadingSpaces(checkLeadingSpacesProperty.GetInt32());
        }
    }

}
