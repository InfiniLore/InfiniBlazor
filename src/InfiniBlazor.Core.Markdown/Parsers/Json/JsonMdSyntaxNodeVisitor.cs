// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace InfiniBlazor.Markdown.Parsers.Json;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class JsonMdSyntaxNodeVisitor<TNode> : IJsonMdSyntaxNodeVisitor where TNode : MdSyntaxNode<TNode>, new() {
    private const string Modifiers = nameof(Modifiers);
    private const string OriginalInput = nameof(OriginalInput);
    private const string Attributes = nameof(Attributes);
    private const string Start = nameof(Start);
    private const string End = nameof(End);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void DeserializeToJson(IMdSyntaxNode node, Utf8JsonWriter writer) {
        DeserializeDetails(Unsafe.As<TNode>(node), writer);
    }

    private static void SerializeModifiers(IMdSyntaxNodeModifier modifiers, Utf8JsonWriter writer) {
        writer.WriteStartObject(Modifiers);

        writer.WriteStartArray(Attributes);
        foreach (KeyValuePair<string, Range> attr in modifiers.Attributes) {
            writer.WriteStartObject();
            writer.WriteString("key", attr.Key);
            writer.WriteNumber(Start, attr.Value.Start.Value);
            writer.WriteNumber(End, attr.Value.End.Value);
            writer.WriteEndObject();
        }

        writer.WriteEndArray();

        writer.WriteString(OriginalInput, modifiers.OriginalInput);
        writer.WriteEndObject();
    }

    protected virtual void DeserializeDetails(TNode node, Utf8JsonWriter writer) {
        if (node.Modifier is {} modifier) {
            SerializeModifiers(modifier, writer);
        }
    }

    public IMdSyntaxNode SerializeToNode(JsonElement element, IMdSyntaxNode parentNode) {
        TNode node = MdSyntaxNodePool<TNode>.Shared.Get();
        parentNode.AddChildNode(node);

        SerializeDetails(element, node);
        return node;
    }

    private static MdSyntaxNodeModifier DeserializeModifiers(JsonElement element)
        => element.TryGetProperty(OriginalInput, out JsonElement originalInputProperty)
            ? MdSyntaxNodeModifier.FromString(originalInputProperty.GetString() ?? string.Empty)
            : MdSyntaxNodeModifier.FromString(string.Empty);

    protected virtual void SerializeDetails(JsonElement element, TNode targetNode) {
        if (element.TryGetProperty(Modifiers, out JsonElement modifiersElement)) {
            targetNode.WithModifier(DeserializeModifiers(modifiersElement));
        }
    }
}
