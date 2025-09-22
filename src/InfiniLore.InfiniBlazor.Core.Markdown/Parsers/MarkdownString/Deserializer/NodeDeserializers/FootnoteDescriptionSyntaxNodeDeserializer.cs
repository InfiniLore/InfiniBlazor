// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
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
