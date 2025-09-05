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
    private const string AltText = nameof(AltText);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ImageMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Href, node.Href);
        targetElement.SetAttributeValue(AltText, node.OriginalAltText);
    }

    protected override void SerializeDetails(XElement element, ImageMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.WithAltText(element.Attribute(AltText)?.Value ?? string.Empty);
        targetNode.WithHref(element.Attribute(Href)?.Value ?? string.Empty); 
    }
}
