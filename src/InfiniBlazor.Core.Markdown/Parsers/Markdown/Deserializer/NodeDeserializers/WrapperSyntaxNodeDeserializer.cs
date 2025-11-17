// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class WrapperSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<WrapperMdSyntaxNode> {

    protected override void Deserialize(WrapperMdSyntaxNode node, StringBuilder builder) {
        builder.Append('<');
        builder.Append(node.Modifier!.OriginalInputSpan);
        builder.Append('>');
        DeserializeChildren(node, builder);
        builder.Append("</>");
    }
}
