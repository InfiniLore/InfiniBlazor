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
    private const string Attributes = nameof(HtmlSpanMdSyntaxNode.Attributes);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(HtmlSpanMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Attributes, node.Attributes);
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, HtmlSpanMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithAttributes(element.Attribute(Attributes)?.Value ?? string.Empty);
    }
}
