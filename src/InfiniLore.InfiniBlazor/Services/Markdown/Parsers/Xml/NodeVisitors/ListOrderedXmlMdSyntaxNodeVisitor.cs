// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListOrderedXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<ListOrderedMdSyntaxNode> {
    private const string LeadingSpaces = nameof(ListOrderedMdSyntaxNode.LeadingSpaces);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ListOrderedMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(LeadingSpaces, node.LeadingSpaces);
    }

    protected override void SerializeDetails(XElement element, ListOrderedMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.LeadingSpaces = int.Parse(element.Attribute(LeadingSpaces)?.Value ?? "0");
    }
}