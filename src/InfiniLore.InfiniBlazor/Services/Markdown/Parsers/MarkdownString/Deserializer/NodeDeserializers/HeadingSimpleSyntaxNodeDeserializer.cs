// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HeadingSimpleSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<HeadingSimpleMdSyntaxNode> {

    protected override void Deserialize(HeadingSimpleMdSyntaxNode node, StringBuilder builder) {
        DeserializeChildren(node, builder);
        builder.Append('\n');
        builder.Append(node.Identifier);
    }
}
