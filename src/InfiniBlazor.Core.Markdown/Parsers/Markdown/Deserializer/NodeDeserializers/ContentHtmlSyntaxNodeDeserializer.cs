// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ContentHtmlSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<HtmlMdSyntaxNode> {

    protected override void Deserialize(HtmlMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.Content);
    }
}
