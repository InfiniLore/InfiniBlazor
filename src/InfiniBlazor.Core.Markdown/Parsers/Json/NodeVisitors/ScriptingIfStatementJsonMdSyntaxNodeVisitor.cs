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
public sealed class ScriptingIfStatementJsonMdSyntaxNodeVisitor : JsonMdSyntaxNodeVisitor<ScriptingIfStatementSyntaxNode> {
    private static readonly string ElseConditionIndex = nameof(ScriptingIfStatementSyntaxNode.ElseConditionIndex).ToCamelCase();
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ScriptingIfStatementSyntaxNode node, Utf8JsonWriter writer) {
        base.DeserializeDetails(node, writer);
        writer.WriteNumber(ElseConditionIndex, node.ElseConditionIndex);
    }
    protected override void SerializeDetails(JsonElement element, ScriptingIfStatementSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        if (element.TryGetProperty(ElseConditionIndex, out JsonElement elseConditionIndexProperty)) {
            targetNode.WithElseConditionIndex(elseConditionIndexProperty.GetInt32());
        }
    }
}
