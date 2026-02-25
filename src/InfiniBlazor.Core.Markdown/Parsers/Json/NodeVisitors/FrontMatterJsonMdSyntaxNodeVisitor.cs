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
public sealed class FrontMatterJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<FrontMatterMdSyntaxNode> {
    private static readonly string Language = nameof(FrontMatterMdSyntaxNode.Language).ToCamelCase();
    private static readonly string Content = nameof(FrontMatterMdSyntaxNode.Content).ToCamelCase();
    private static readonly string LeadingSpaces = nameof(FrontMatterMdSyntaxNode.LeadingSpaces).ToCamelCase();
    private static readonly string DashesCount = nameof(FrontMatterMdSyntaxNode.DashesCount).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(FrontMatterMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteString(Language, node.Language);
        writer.WriteString(Content, node.Content);
        writer.WriteString(LeadingSpaces, node.LeadingSpaces.ToString());
        writer.WriteString(DashesCount, node.DashesCount.ToString());
    }

    protected override void SerializeDetails(JsonElement element, FrontMatterMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(Language, out JsonElement languageProperty)) {
            targetNode.WithLanguage(languageProperty.GetString() ?? string.Empty);
        }

        if (element.TryGetProperty(Content, out JsonElement contentProperty)) {
            targetNode.WithContent(contentProperty.GetString() ?? string.Empty);
        }
        
        if (element.TryGetProperty(LeadingSpaces, out JsonElement leadingSpacesProperty)) {
            targetNode.WithLeadingSpaces(leadingSpacesProperty.GetInt32());
        }
        
        if (element.TryGetProperty(DashesCount, out JsonElement dashesCountProperty)) {
            targetNode.WithDashesCount(dashesCountProperty.GetInt32());
        }
    }
}
