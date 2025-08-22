// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ParagraphSyntaxNodeDeserializer :  BaseMarkdownStringMdSyntaxNodeDeserializer<ParagraphMdSyntaxNode> {
    protected override void Deserialize(ParagraphMdSyntaxNode node, StringBuilder builder) {
        DeserializeChildren(node, builder);
        builder.Append('\n');
    }
}
