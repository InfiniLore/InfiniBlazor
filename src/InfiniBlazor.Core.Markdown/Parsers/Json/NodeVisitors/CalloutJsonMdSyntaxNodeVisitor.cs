// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.Json;

namespace InfiniBlazor.Markdown.Parsers.Json.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CalloutJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<CalloutMdSyntaxNode> {
    private static readonly string CalloutType = nameof(CalloutMdSyntaxNode.CalloutType).ToCamelCase();
    private static readonly string CollapsedState = nameof(CalloutMdSyntaxNode.CollapsedState).ToCamelCase();
    private static readonly string LeadingSpaces = nameof(CalloutMdSyntaxNode.LeadingSpaces).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(CalloutMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteString(CalloutType, node.CalloutType);
        writer.WriteString(CollapsedState, node.CollapsedState.ToString());
        writer.WriteNumber(LeadingSpaces, node.LeadingSpaces);
    }

    protected override void SerializeDetails(JsonElement element, CalloutMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(LeadingSpaces, out JsonElement leadingSpacesProperty)) {
            targetNode.WithLeadingSpaces(leadingSpacesProperty.GetInt32());
        }

        if (element.TryGetProperty(CalloutType, out JsonElement calloutTypeProperty)) {
            targetNode.WithCalloutType(calloutTypeProperty.GetString() ?? string.Empty);
        }

        // ReSharper disable once InvertIf
        if (element.TryGetProperty(CollapsedState, out JsonElement collapsedStateProperty)) {
            string? collapsedStateValue = collapsedStateProperty.GetString();
            if (Enum.TryParse(collapsedStateValue, out CalloutMdSyntaxNode.CollapseStateOptions value)) {
                targetNode.WithCollapseState(value);
            }
        }
    }

}
