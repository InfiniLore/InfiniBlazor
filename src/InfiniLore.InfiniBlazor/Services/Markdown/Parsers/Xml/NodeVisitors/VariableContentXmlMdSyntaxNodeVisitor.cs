// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class VariableContentXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<VariableContentMdSyntaxNode> {
    private const string BracesCount = nameof(VariableContentMdSyntaxNode.BracesCount);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(VariableContentMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(BracesCount, node.BracesCount);
        targetElement.Value = node.Content;
    }

    protected override void SerializeDetails(XElement element, VariableContentMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.Content = element.Value;
        targetNode.BracesCount = int.Parse(element.Attribute(BracesCount)?.Value ?? "0");  
    }
}
