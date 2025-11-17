// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HorizontalRuleSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<HorizontalRuleMdSyntaxNode> {
    protected override void Deserialize(HorizontalRuleMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.Identifier);
    }
}
