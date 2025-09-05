// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EmoteSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<EmoteMdSyntaxNode> {
    protected override void Deserialize(EmoteMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.OriginalEmote);
    }
}
