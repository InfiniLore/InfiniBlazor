// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EscapedCharacterSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<EscapedCharacterMdSyntaxNode> {

    protected override void Deserialize(EscapedCharacterMdSyntaxNode node, StringBuilder builder) {
        builder.Append('\\');
        builder.Append(node.Content);
    }
}
