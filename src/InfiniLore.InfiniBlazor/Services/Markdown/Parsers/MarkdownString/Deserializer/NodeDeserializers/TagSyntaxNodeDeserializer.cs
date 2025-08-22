// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TagSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<TagMdSyntaxNode> {
    protected override void Deserialize(TagMdSyntaxNode node, StringBuilder builder) {
        builder.Append('#');
        builder.Append(node.ContentTag);
    }
}
