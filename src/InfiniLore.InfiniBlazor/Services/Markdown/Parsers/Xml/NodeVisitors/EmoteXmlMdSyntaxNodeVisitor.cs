// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
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
    protected override void SerializeDetails(EmoteMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.SetAttributeValue(EmoteKey, node.EmoteKey);
        targetElement.SetAttributeValue(OriginalEmote, node.OriginalEmote);
    }

    protected override void DeserializeDetails(XElement element, EmoteMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.EmoteKey = element.Attribute(EmoteKey)?.Value ?? string.Empty;
        targetNode.OriginalEmote = element.Attribute(OriginalEmote)?.Value ?? string.Empty;
    }
}
