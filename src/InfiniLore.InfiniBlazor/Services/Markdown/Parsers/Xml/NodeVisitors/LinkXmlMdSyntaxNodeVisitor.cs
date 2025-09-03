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
    protected override void DeserializeDetails(LinkMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Href, node.Href);
    }

    protected override void SerializeDetails(XElement element, LinkMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.WithHref(element.Attribute(Href)?.Value ?? string.Empty);
    }
}
