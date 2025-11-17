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

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, TemplateMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithContent(element.Value);
        targetNode.WithBracesCount(int.Parse(element.Attribute(BracesCount)?.Value ?? "0"));
    }
}
