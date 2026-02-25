// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ScriptingExpressionSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<ScriptingExpressionSyntaxNode> {

    protected override void Deserialize(ScriptingExpressionSyntaxNode node, StringBuilder builder) {
        builder.AppendLine(node.FullStatement);
        DeserializeChildren(node, builder);
    }
}
