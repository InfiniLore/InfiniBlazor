// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ContentSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<TextMdSyntaxNode> {
    protected override void Deserialize(TextMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.Content);
    }
}
