// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class UnderlineSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<UnderlineMdSyntaxNode> {
    protected override void Deserialize(UnderlineMdSyntaxNode node, StringBuilder builder) {
        builder.Append('_');
        DeserializeChildren(node, builder);
        builder.Append('_');
    }
}

