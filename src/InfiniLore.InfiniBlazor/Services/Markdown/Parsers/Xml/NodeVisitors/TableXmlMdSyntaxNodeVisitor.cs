// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TableXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<TableMdSyntaxNode> {
    private const string HeaderIndex = nameof(TableMdSyntaxNode.HeaderIndex);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(TableMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.SetAttributeValue(HeaderIndex, node.HeaderIndex);
    }

    protected override void DeserializeDetails(XElement element, TableMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.HeaderIndex = int.Parse(element.Attribute(HeaderIndex)?.Value ?? "0");   
    }
}
