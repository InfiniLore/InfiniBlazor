// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.Json;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Json.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeBlockJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<CodeBlockMdSyntaxNode> {
    private const string Language = nameof(CodeBlockMdSyntaxNode.Language);
    private const string Content = nameof(CodeBlockMdSyntaxNode.Content);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(CodeBlockMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);
        
        writer.WriteString(Language, node.Language);
        writer.WriteString(Content, node.Content);
    }

    protected override void SerializeDetails(JsonElement element, CodeBlockMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        
        if (element.TryGetProperty(Language, out JsonElement languageProperty)) {
            targetNode.WithLanguage(languageProperty.GetString() ?? string.Empty);
        }
        
        if (element.TryGetProperty(Content, out JsonElement contentProperty)) {
            targetNode.WithContent(contentProperty.GetString() ?? string.Empty);
        }
    }
}
