// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ContentHtmlSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<HtmlMdSyntaxNode> {

    protected override void Deserialize(HtmlMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.Content);
    }
}