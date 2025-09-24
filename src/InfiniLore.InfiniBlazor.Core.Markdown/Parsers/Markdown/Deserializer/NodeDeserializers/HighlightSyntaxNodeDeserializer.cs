// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HighlightSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<HighlightMdSyntaxNode> {

    protected override void Deserialize(HighlightMdSyntaxNode node, StringBuilder builder) {
        builder.Append("==");
        DeserializeChildren(node, builder);
        builder.Append("==");
    }
}