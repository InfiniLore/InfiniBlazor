// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeInlineSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<CodeInlineMdSyntaxNode> {
    protected override void Deserialize(CodeInlineMdSyntaxNode node, StringBuilder builder) {
        builder.Append('`', Math.Max(node.BackTickCount, 1));
        builder.Append(node.Content);
        builder.Append('`', Math.Max(node.BackTickCount, 1));
    }
}
