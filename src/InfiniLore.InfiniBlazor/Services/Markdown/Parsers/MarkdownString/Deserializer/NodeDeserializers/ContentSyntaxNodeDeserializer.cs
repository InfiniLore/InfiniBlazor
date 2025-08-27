// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ContentSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<ContentMdSyntaxNode> {
    protected override void Deserialize(ContentMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.Content);
    }
}
