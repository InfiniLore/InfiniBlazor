// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class LinkXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<LinkMdSyntaxNode> {
    private const string Href = nameof(ImageMdSyntaxNode.Href);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(LinkMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Href, node.Href);
    }

    protected override void DeserializeDetails(XElement element, LinkMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.Href = element.Attribute(Href)?.Value ?? string.Empty;
    }
}
