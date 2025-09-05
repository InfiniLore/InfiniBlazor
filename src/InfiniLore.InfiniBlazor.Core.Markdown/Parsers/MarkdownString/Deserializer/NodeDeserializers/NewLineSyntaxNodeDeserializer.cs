// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class NewLineSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<NewLineMdSyntaxNode> {

    protected override void Deserialize(NewLineMdSyntaxNode node, StringBuilder builder) {
        builder.Append('\n');
    }
}
