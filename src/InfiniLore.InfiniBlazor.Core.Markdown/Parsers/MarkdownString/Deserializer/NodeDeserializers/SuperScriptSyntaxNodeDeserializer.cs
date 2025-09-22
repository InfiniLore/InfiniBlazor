// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class SuperScriptSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<SuperScriptMdSyntaxNode> {
    protected override void Deserialize(SuperScriptMdSyntaxNode node, StringBuilder builder) {
        builder.Append('^');
        DeserializeChildren(node, builder);
        builder.Append('^');
    }
}
