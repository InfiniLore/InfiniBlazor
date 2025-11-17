// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TagSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<TagMdSyntaxNode> {
    protected override void Deserialize(TagMdSyntaxNode node, StringBuilder builder) {
        builder.Append('#');
        builder.Append(node.Content);
    }
}
