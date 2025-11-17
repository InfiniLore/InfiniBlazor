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
public sealed class LinkJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<LinkMdSyntaxNode> {
    private static readonly string Href = nameof(LinkMdSyntaxNode.Href).ToCamelCase();
    private static readonly string Title = nameof(LinkMdSyntaxNode.Title).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(LinkMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        if (node.Href.IsNotNullOrEmpty()) writer.WriteString(Href, node.Href);
        if (node.Title.IsNotNullOrEmpty()) writer.WriteString(Title, node.Title);
    }

    protected override void SerializeDetails(JsonElement element, LinkMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(Href, out JsonElement hrefProperty)) {
            targetNode.WithHref(hrefProperty.GetString() ?? string.Empty);
        }

        if (element.TryGetProperty(Title, out JsonElement titleProperty)) {
            targetNode.WithTitle(titleProperty.GetString() ?? string.Empty);
        }
    }
}
