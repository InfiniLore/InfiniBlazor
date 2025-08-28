// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ImageSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<ImageMdSyntaxNode> {
    protected override void Deserialize(ImageMdSyntaxNode node, StringBuilder builder) {
        builder.Append('!');
        builder.Append('[');
        builder.Append(node.OriginalAltText);
        builder.Append(']');
        builder.Append('(');
        builder.Append(node.Href);
        if (node.TryGetModifier(out IMdSyntaxNodeModifier? modifier)) builder.Append(modifier.OriginalInputSpan);
        builder.Append(')');
    }
}
