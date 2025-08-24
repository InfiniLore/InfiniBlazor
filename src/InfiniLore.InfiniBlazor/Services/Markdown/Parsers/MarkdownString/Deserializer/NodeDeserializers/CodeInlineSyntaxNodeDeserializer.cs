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
        builder.Append('`', Math.Min(node.BackTickCount, 1));
        builder.Append(node.OriginalContentCode);
        builder.Append('`', Math.Min(node.BackTickCount, 1));
    }
}
