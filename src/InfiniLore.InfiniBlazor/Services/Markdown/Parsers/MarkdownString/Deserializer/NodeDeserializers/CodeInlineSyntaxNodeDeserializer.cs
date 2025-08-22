// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeInlineSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<CodeInlineMdSyntaxNode> {
    protected override void Deserialize(CodeInlineMdSyntaxNode node, StringBuilder builder) {
        builder.Append('`', node.BackTickCount);
        builder.Append(node.ContentCode);
        builder.Append('`', node.BackTickCount);
    }
}
