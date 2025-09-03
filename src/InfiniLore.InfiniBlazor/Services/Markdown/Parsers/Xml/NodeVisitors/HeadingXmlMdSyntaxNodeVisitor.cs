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
    protected override void DeserializeDetails(HeadingMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Level, node.Level);
    }

    protected override void SerializeDetails(XElement element, HeadingMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.WithLevel(int.Parse(element.Attribute(Level)?.Value ?? "0"));   
    }
}
