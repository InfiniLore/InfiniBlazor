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
public sealed class TemplateJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<TemplateMdSyntaxNode> {
    private static readonly string BracesCount = nameof(TemplateMdSyntaxNode.BracesCount).ToCamelCase();
    private static readonly string Content = nameof(TemplateMdSyntaxNode.Content).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(TemplateMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteNumber(BracesCount, node.BracesCount);
        writer.WriteString(Content, node.Content);
    }

    protected override void SerializeDetails(JsonElement element, TemplateMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(Content, out JsonElement contentProperty)) {
            targetNode.WithContent(contentProperty.GetString() ?? string.Empty);
        }

        if (element.TryGetProperty(BracesCount, out JsonElement bracesCountProperty)) {
            targetNode.WithBracesCount(bracesCountProperty.GetInt32());
        }
    }

}
