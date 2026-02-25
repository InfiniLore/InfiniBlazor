// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ScriptingIfStatementSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<ScriptingIfStatementSyntaxNode> {
    protected override void Deserialize(ScriptingIfStatementSyntaxNode node, StringBuilder builder) {
        DeserializeChildren(node, builder);
        builder.Append("@endif");
    }
}
