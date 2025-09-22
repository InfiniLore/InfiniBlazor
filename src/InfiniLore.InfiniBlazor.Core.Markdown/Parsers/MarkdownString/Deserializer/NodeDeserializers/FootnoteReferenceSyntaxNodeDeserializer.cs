// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class FootnoteReferenceSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<FootnoteReferenceMdSyntaxNode> {
    protected override void Deserialize(FootnoteReferenceMdSyntaxNode node, StringBuilder builder) {
        builder.Append('[');
        builder.Append('^');
        builder.Append(node.Identifier);
        builder.Append(']');
    }
}
