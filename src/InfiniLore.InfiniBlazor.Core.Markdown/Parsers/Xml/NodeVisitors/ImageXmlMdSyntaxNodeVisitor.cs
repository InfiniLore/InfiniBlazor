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
    private const string Title = nameof(ImageMdSyntaxNode.Title);
    private const string AltText = nameof(AltText);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ImageMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Href, node.Href);
        targetElement.SetAttributeValue(AltText, node.OriginalAltText);
        if (node.Title.IsNotNullOrEmpty()) targetElement.SetAttributeValue(Title, node.Title);
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, ImageMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithAltText(element.Attribute(AltText)?.Value ?? string.Empty);
        targetNode.WithHref(element.Attribute(Href)?.Value ?? string.Empty); 
    }
}
