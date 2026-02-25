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
public sealed class CodeInlineJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<CodeInlineMdSyntaxNode> {
    private static readonly string BackTickCount = nameof(CodeInlineMdSyntaxNode.BackTickCount).ToCamelCase();
    private static readonly string Content = nameof(CodeInlineMdSyntaxNode.Content).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(CodeInlineMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteNumber(BackTickCount, node.BackTickCount);
        writer.WriteString(Content, node.Content);
    }

    protected override void SerializeDetails(JsonElement element, CodeInlineMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(BackTickCount, out JsonElement backTickCountProperty)) {
            targetNode.WithBackTickCount(backTickCountProperty.GetInt32());
        }

        if (element.TryGetProperty(Content, out JsonElement contentProperty)) {
            targetNode.WithContent(contentProperty.GetString() ?? string.Empty);
        }
    }

}
