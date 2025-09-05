// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class StrikeSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<StrikeMdSyntaxNode> {
    protected override void Deserialize(StrikeMdSyntaxNode node, StringBuilder builder) {
        builder.Append('~');
        DeserializeChildren(node, builder);
        builder.Append('~');
    }
}
