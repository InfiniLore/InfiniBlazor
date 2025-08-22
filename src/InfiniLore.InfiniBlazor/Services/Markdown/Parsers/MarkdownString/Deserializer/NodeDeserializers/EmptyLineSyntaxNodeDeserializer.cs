// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EmptyLineSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<EmptyLineMdSyntaxNode> {

    protected override void Deserialize(EmptyLineMdSyntaxNode node, StringBuilder builder) {
        builder.Append('\n');
    }
}
