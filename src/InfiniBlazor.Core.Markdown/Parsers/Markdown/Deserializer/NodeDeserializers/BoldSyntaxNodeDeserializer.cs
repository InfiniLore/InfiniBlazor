// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class BoldSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<BoldMdSyntaxNode> {

    protected override void Deserialize(BoldMdSyntaxNode node, StringBuilder builder) {
        builder.Append("**");
        DeserializeChildren(node, builder);
        builder.Append("**");
    }
}
