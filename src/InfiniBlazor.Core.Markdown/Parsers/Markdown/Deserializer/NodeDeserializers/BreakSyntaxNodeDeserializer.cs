// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class BreakSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<BreakMdSyntaxNode> {

    protected override void Deserialize(BreakMdSyntaxNode node, StringBuilder builder) {
        builder.Append("<br/>");
    }
}
