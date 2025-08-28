// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HtmlSpanSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<HtmlSpanMdSyntaxNode> {

    protected override void Deserialize(HtmlSpanMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.TagValue);
        DeserializeChildren(node, builder);
        builder.Append("</span>");
    }
}