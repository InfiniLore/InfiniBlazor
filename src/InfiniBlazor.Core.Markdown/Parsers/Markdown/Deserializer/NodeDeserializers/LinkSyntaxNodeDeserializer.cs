// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
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
        if (node.Title.IsNotNullOrEmpty()) {
            builder.Append(' ');
            builder.Append('"');
            builder.Append(node.Title);
            builder.Append('"');
        }
        if (node.Modifier is { OriginalInputSpan: var inputSpan }) builder.Append(inputSpan);
        builder.Append(')');
    }
}
