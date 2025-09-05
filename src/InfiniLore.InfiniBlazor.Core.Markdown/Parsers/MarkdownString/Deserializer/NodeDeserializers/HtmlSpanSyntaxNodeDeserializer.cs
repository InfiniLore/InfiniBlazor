// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HtmlSpanSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<HtmlSpanMdSyntaxNode> {

    protected override void Deserialize(HtmlSpanMdSyntaxNode node, StringBuilder builder) {
        builder.Append("<span");
        if (node.Attributes.IsNotNullOrEmpty()) {
            builder.Append(' ');
            builder.Append(node.Attributes);
        }
        builder.Append('>');
        DeserializeChildren(node, builder);
        builder.Append("</span>");
    }
}