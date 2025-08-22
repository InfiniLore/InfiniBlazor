// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class SuperScriptSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<SubScriptMdSyntaxNode> {
    protected override void Deserialize(SubScriptMdSyntaxNode node, StringBuilder builder) {
        builder.Append('^');
        builder.Append('^');
        DeserializeChildren(node, builder);
        builder.Append('^');
        builder.Append('^');
    }
}
