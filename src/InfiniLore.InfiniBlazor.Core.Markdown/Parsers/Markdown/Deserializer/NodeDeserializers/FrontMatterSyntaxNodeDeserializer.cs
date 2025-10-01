// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class FrontMatterSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<FrontMatterMdSyntaxNode> {
    protected override void Deserialize(FrontMatterMdSyntaxNode node, StringBuilder builder) {
        builder.Append('-', node.DashesCount);
        builder.Append(' ', node.LeadingSpaces);
        builder.Append(node.Language);
        builder.Append('\n');
        builder.Append(node.Content);
        builder.Append('\n');
        builder.Append('-', node.DashesCount);
    }
}
