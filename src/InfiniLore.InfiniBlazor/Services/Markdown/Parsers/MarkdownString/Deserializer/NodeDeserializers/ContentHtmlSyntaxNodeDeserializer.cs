// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ContentHtmlSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<ContentHtmlMdSyntaxNode> {

    protected override void Deserialize(ContentHtmlMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.ContentHtml);
    }
}