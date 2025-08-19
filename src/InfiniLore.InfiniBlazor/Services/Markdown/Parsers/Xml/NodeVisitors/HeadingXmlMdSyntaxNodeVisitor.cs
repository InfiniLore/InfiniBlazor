// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HeadingXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<HeadingMdSyntaxNode> {
    private const string Level = nameof(HeadingMdSyntaxNode.Level);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(HeadingMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Level, node.Level);
    }

    protected override void DeserializeDetails(XElement element, HeadingMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.Level = int.Parse(element.Attribute(Level)?.Value ?? "0");   
    }
}
