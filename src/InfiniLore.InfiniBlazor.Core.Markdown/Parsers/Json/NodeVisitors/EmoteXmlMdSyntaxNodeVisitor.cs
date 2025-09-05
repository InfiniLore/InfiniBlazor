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
public sealed class EmoteJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<EmoteMdSyntaxNode> {
    private static readonly string EmoteKey = nameof(EmoteMdSyntaxNode.EmoteKey).ToCamelCase();
    private static readonly string OriginalEmote = nameof(EmoteMdSyntaxNode.OriginalEmote).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(EmoteMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        writer.WriteString(EmoteKey, node.EmoteKey);
        writer.WriteString(OriginalEmote, node.OriginalEmote);
    }

    protected override void SerializeDetails(JsonElement element, EmoteMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        if (element.TryGetProperty(EmoteKey, out JsonElement emoteKeyProperty)) {
            targetNode.WithEmoteKey(emoteKeyProperty.GetString() ?? string.Empty);
        }

        if (element.TryGetProperty(OriginalEmote, out JsonElement originalEmoteProperty)) {
            targetNode.WithOriginalEmote(originalEmoteProperty.GetString() ?? string.Empty);
        }
    }

}
