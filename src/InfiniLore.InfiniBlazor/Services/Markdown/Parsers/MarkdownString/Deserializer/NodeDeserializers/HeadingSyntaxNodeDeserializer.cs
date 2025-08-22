// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HeadingSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<HeadingMdSyntaxNode> {
    protected override void Deserialize(HeadingMdSyntaxNode node, StringBuilder builder) {
        builder.Append('#', node.Level);
        builder.Append(' ');
        
        DeserializeChildren(node, builder);
        
        if (node.HasNextSibling()) builder.Append('\n');
    }
}
