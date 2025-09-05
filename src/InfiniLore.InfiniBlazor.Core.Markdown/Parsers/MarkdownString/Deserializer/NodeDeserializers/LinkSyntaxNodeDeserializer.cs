// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class LinkSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<LinkMdSyntaxNode> {
    protected override void Deserialize(LinkMdSyntaxNode node, StringBuilder builder) {
        builder.Append('[');
        DeserializeChildren(node, builder);
        builder.Append(']');
        builder.Append('(');
        builder.Append(node.Href);
        builder.Append(')');
    }
}
