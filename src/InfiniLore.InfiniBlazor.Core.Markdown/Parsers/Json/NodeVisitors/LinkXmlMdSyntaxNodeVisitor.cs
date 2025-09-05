// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using CodeOfChaos.GeneratorTools;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.Json;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Json.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class LinkJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<LinkMdSyntaxNode> {
    public static readonly string Href = nameof(LinkMdSyntaxNode.Href).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(LinkMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);
        
        if (node.Href.IsNotNullOrEmpty()) writer.WriteString(Href, node.Href);
    }

    protected override void SerializeDetails(JsonElement element, LinkMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        
        if (element.TryGetProperty(Href, out JsonElement hrefProperty)) {
            targetNode.WithHref(hrefProperty.GetString() ?? string.Empty);
        }
    }
}

