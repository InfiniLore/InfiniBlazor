// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
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

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, HeadingMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithLevel(int.Parse(element.Attribute(Level)?.Value ?? "0"));
    }
}
