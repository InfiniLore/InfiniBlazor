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
    protected override void DeserializeDetails(EmoteMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(EmoteKey, node.EmoteKey);
        targetElement.SetAttributeValue(OriginalEmote, node.OriginalEmote);
    }

    protected override void SerializeDetails(XElement element, EmoteMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.EmoteKey = element.Attribute(EmoteKey)?.Value ?? string.Empty;
        targetNode.OriginalEmote = element.Attribute(OriginalEmote)?.Value ?? string.Empty;
    }
}
