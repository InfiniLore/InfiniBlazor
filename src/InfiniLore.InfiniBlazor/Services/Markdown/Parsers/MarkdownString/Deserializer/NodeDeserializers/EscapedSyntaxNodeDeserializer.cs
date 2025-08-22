// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EscapedSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<EscapedCharacterMdSyntaxNode> {

    protected override void Deserialize(EscapedCharacterMdSyntaxNode node, StringBuilder builder) {
        builder.Append('\\');
        builder.Append(node.ContentChar);
    }
}
