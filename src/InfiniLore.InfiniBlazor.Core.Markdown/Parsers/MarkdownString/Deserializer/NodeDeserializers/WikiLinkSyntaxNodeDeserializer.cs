// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
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
