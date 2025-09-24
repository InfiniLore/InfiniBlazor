// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class UserSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<UserMdSyntaxNode> {
    protected override void Deserialize(UserMdSyntaxNode node, StringBuilder builder) {
        builder.Append('@');
        builder.Append(node.Content);
    }
}
