// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class SubScriptSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<SubScriptMdSyntaxNode> {
    protected override void Deserialize(SubScriptMdSyntaxNode node, StringBuilder builder) {
        builder.Append('~');
        DeserializeChildren(node, builder);
        builder.Append('~');
    }
}
