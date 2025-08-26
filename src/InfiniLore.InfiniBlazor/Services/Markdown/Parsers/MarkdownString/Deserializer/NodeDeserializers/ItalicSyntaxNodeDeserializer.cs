// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ItalicSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<ItalicMdSyntaxNode> {

    protected override void Deserialize(ItalicMdSyntaxNode node, StringBuilder builder) {
        builder.Append('*');
        DeserializeChildren(node, builder);
        builder.Append('*');
    }
}
