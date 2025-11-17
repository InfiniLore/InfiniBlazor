// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EmoteSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<EmoteMdSyntaxNode> {
    protected override void Deserialize(EmoteMdSyntaxNode node, StringBuilder builder) {
        builder.Append(node.OriginalEmote);
    }
}
