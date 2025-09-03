// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class VariableXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<VariableMdSyntaxNode> {
    private const string BracesCount = nameof(VariableMdSyntaxNode.BracesCount);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(VariableMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(BracesCount, node.BracesCount);
        targetElement.Value = node.Content;
    }

    protected override void SerializeDetails(XElement element, VariableMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.Content = element.Value;
        targetNode.BracesCount = int.Parse(element.Attribute(BracesCount)?.Value ?? "0");  
    }
}
