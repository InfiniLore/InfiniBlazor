// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeBlockSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<CodeBlockMdSyntaxNode> {
    protected override void Deserialize(CodeBlockMdSyntaxNode node, StringBuilder builder) {
        builder.Append("```");
        builder.Append(node.Language);
        builder.Append('\n');
        builder.Append(node.Content);
        builder.Append("```");
    }
}
