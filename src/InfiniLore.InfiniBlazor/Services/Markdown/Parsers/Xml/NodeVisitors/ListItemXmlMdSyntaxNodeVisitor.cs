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
    private const string IsChecked = nameof(ListItemMdSyntaxNode.IsChecked);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(ListItemMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.SetAttributeValue(IsCheckable, node.IsCheckable);
        targetElement.SetAttributeValue(IsChecked, node.IsChecked);
    }

    protected override void DeserializeDetails(XElement element, ListItemMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.IsCheckable = bool.Parse(element.Attribute(IsCheckable)?.Value ?? "0");
        targetNode.IsChecked = bool.Parse(element.Attribute(IsChecked)?.Value ?? "0");
    }
}
