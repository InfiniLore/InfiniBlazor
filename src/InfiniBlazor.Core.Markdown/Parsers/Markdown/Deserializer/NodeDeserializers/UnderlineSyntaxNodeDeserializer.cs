// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class UnderlineSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<UnderlineMdSyntaxNode> {
    protected override void Deserialize(UnderlineMdSyntaxNode node, StringBuilder builder) {
        builder.Append('_');
        DeserializeChildren(node, builder);
        builder.Append('_');
    }
}
