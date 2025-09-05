// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.Json;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Json.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class LinkJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<LinkMdSyntaxNode> {
    protected override void DeserializeDetails(LinkMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);
        
        if (node.Href.IsNotNullOrEmpty()) writer.WriteString("href", node.Href);
    }

    protected override void SerializeDetails(JsonElement element, LinkMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        
        if (element.TryGetProperty("href", out JsonElement hrefProperty)) {
            targetNode.WithHref(hrefProperty.GetString() ?? string.Empty);
        }
    }
}

