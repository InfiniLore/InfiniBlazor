// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HorizontalRuleSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<HorizontalRuleMdSyntaxNode> {
    protected override void Deserialize(HorizontalRuleMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.Identifier);
        if (node.HasNextSibling()) builder.Append('\n');
    }
}
