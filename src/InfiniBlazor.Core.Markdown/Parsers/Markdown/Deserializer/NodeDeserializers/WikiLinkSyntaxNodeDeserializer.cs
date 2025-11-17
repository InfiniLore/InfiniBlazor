// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class WikiLinkSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<WikiLinkMdSyntaxNode> {
    protected override void Deserialize(WikiLinkMdSyntaxNode node, StringBuilder builder) {
        builder.Append('[');
        builder.Append('[');
        builder.Append(node.Content);
        builder.Append(']');
        builder.Append(']');
    }
}
