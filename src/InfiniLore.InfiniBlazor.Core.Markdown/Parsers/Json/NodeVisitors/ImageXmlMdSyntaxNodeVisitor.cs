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
public sealed class ImageJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<ImageMdSyntaxNode> {
    private static readonly string Href = nameof(ImageMdSyntaxNode.Href).ToCamelCase();
    private static readonly string Title = nameof(ImageMdSyntaxNode.Title).ToCamelCase();
    private static readonly string AltText = nameof(AltText).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ImageMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteString(Href, node.Href);
        writer.WriteString(Title, node.Title);
        writer.WriteString(AltText, node.OriginalAltText);
    }

    protected override void SerializeDetails(JsonElement element, ImageMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(AltText, out JsonElement altTextProperty)) {
            targetNode.WithAltText(altTextProperty.GetString() ?? string.Empty);
        }

        if (element.TryGetProperty(Href, out JsonElement hrefProperty)) {
            targetNode.WithHref(hrefProperty.GetString() ?? string.Empty);
        }

        if (element.TryGetProperty(Title, out JsonElement titleProperty)) {
            targetNode.WithTitle(titleProperty.GetString() ?? string.Empty);
        }
    }

}
