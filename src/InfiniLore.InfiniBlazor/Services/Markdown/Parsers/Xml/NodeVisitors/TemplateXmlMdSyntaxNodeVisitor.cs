// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TemplateXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<TemplateMdSyntaxNode> {
    private const string BracesCount = nameof(TemplateMdSyntaxNode.BracesCount);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(TemplateMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(BracesCount, node.BracesCount);
        targetElement.Value = node.Content;
    }

    protected override void SerializeDetails(XElement element, TemplateMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.WithContent(element.Value);
        targetNode.WithBracesCount(int.Parse(element.Attribute(BracesCount)?.Value ?? "0"));
    }
}
