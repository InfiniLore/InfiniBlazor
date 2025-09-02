// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class WikiLinkXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<WikiLinkMdSyntaxNode> {
    private const string Href = nameof(ImageMdSyntaxNode.Href);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(WikiLinkMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Href, node.Href);
    }

    protected override void SerializeDetails(XElement element, WikiLinkMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.Href = element.Attribute(Href)?.Value ?? string.Empty;
    }
}
