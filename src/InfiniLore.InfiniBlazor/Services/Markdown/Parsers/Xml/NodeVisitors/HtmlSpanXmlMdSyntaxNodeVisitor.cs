// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HtmlSpanXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<HtmlSpanMdSyntaxNode> {
    private const string TagValue = nameof(HtmlSpanMdSyntaxNode.TagValue);
    private const string Attributes = nameof(HtmlSpanMdSyntaxNode.Attributes);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(HtmlSpanMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.SetAttributeValue(TagValue, node.TagValue);
        targetElement.SetAttributeValue(Attributes, node.Attributes);
    }

    protected override void DeserializeDetails(XElement element, HtmlSpanMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.TagValue = element.Attribute(TagValue)?.Value ?? string.Empty;
        targetNode.Attributes = element.Attribute(Attributes)?.Value ?? string.Empty;
    }
}
