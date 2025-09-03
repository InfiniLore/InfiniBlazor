// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class VariableContentSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<VariableContentMdSyntaxNode> {
    protected override void Deserialize(VariableContentMdSyntaxNode node, StringBuilder builder) {
        builder.Append('{', Math.Max(node.BracesCount, 1));
        builder.Append(node.Content);
        builder.Append('}', Math.Max(node.BracesCount, 1));
    }
}
