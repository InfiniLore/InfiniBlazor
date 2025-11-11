// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EmoteXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<EmoteMdSyntaxNode> {
    private const string EmoteKey = nameof(EmoteMdSyntaxNode.EmoteKey);
    private const string OriginalEmote = nameof(EmoteMdSyntaxNode.OriginalEmote);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(EmoteMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(EmoteKey, node.EmoteKey);
        targetElement.SetAttributeValue(OriginalEmote, node.OriginalEmote);
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, EmoteMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithEmoteKey(element.Attribute(EmoteKey)?.Value ?? string.Empty);
        targetNode.WithOriginalEmote(element.Attribute(OriginalEmote)?.Value ?? string.Empty);
    }
}
