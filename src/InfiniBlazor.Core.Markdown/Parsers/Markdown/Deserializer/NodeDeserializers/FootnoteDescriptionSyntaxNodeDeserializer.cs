// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class FootnoteDescriptionSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<FootnoteDescriptionMdSyntaxNode> {
    protected override void Deserialize(FootnoteDescriptionMdSyntaxNode node, StringBuilder builder) {
        builder.Append('[');
        builder.Append('^');
        builder.Append(node.Identifier);
        builder.Append(']');
        builder.Append(' ');
        builder.Append(':');
        builder.Append(' ');
        DeserializeChildren(node, builder);
    }
}
