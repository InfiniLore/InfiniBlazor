// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ImageXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<ImageMdSyntaxNode> {
    private const string Href = nameof(ImageMdSyntaxNode.Href);
    private const string AltText = nameof(ImageMdSyntaxNode.AltText);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(ImageMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Href, node.Href);
        targetElement.SetAttributeValue(AltText, node.AltText);
    }

    protected override void DeserializeDetails(XElement element, ImageMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.Href = element.Attribute(Href)?.Value ?? string.Empty;
        targetNode.AltText = element.Attribute(AltText)?.Value ?? string.Empty;
    }
}
