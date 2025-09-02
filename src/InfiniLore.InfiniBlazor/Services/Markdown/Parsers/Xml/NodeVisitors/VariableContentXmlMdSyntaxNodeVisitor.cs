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
    private const string Variable = nameof(VariableContentMdSyntaxNode.Variable);
    private const string BracesCount = nameof(VariableContentMdSyntaxNode.Variable);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(VariableContentMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Variable, node.Variable);
        targetElement.SetAttributeValue(BracesCount, node.BracesCount);
    }

    protected override void SerializeDetails(XElement element, VariableContentMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.Variable = element.Attribute(Variable)?.Value ?? string.Empty;
        targetNode.BracesCount = int.Parse(element.Attribute(BracesCount)?.Value ?? "0");  
    }
}
