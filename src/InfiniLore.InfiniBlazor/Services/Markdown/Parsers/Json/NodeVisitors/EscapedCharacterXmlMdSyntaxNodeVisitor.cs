// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.Json;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Json.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EscapedCharacterJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<EscapedCharacterMdSyntaxNode> {
    private const string Content = nameof(EscapedCharacterMdSyntaxNode.Content);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(EscapedCharacterMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteString(Content, node.Content.ToString());
    }

    protected override void SerializeDetails(JsonElement element, EscapedCharacterMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        // ReSharper disable once InvertIf
        if (element.TryGetProperty(Content, out JsonElement contentProperty)) {
            string? contentValue = contentProperty.GetString();
            if (!string.IsNullOrEmpty(contentValue)) {
                targetNode.WithContent(contentValue[0]);
            }
        }
    }

}
