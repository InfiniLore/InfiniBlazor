// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListItemXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<ListItemMdSyntaxNode> {
    private const string IsCheckable = nameof(ListItemMdSyntaxNode.IsCheckable);
    private const string CheckMarker = nameof(CheckMarker);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ListItemMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(IsCheckable, node.IsCheckable);
        targetElement.SetAttributeValue(CheckMarker, node.OriginalCheckMarker);
    }

    protected override void SerializeDetails(XElement element, ListItemMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.IsCheckable = bool.Parse(element.Attribute(IsCheckable)?.Value ?? "0");
        targetNode.OriginalCheckMarker = element.Attribute(CheckMarker)?.Value ?? string.Empty;
    }
}
