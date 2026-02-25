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
public sealed class TableJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<TableMdSyntaxNode> {
    private static readonly string Alignments = nameof(TableMdSyntaxNode.Alignments).ToCamelCase();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(TableMdSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);

        // ReSharper disable once InvertIf
        if (node.HasAlignments) {
            writer.WriteStartArray(Alignments);
            int columnCount = node.GetHeaderCells().Length;
            foreach (TableMdSyntaxNode.Alignment alignment in node.Alignments.AsSpan(0, columnCount)) {
                writer.WriteStringValue(Enum.GetName(alignment) ?? alignment.ToString());
            }

            writer.WriteEndArray();
        }
    }

    protected override void SerializeDetails(JsonElement element, TableMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);

        // ReSharper disable once InvertIf
        if (element.TryGetProperty(Alignments, out JsonElement alignmentsProperty) && alignmentsProperty.ValueKind == JsonValueKind.Array) {
            JsonElement[] alignmentArray = alignmentsProperty.EnumerateArray().ToArray();
            if (alignmentArray.Length <= 0) return;

            Span<TableMdSyntaxNode.Alignment> alignmentValues = stackalloc TableMdSyntaxNode.Alignment[alignmentArray.Length];
            for (int i = 0; i < alignmentArray.Length; i++) {
                string? alignmentString = alignmentArray[i].GetString();
                if (!string.IsNullOrEmpty(alignmentString) &&
                    Enum.TryParse(alignmentString, out TableMdSyntaxNode.Alignment alignment)) {
                    alignmentValues[i] = alignment;
                }
            }

            targetNode.WithAlignments(alignmentValues);
        }
    }

}
