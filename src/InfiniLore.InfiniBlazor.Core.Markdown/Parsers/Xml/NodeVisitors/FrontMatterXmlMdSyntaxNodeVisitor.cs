// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class FrontMatterXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<FrontMatterMdSyntaxNode> {
    private const string Language = nameof(FrontMatterMdSyntaxNode.Language);
    private const string DashesCount = nameof(FrontMatterMdSyntaxNode.DashesCount);
    private const string LeadingSpaces = nameof(FrontMatterMdSyntaxNode.LeadingSpaces);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(FrontMatterMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        AddXmlPreserveSpace(targetElement);
        targetElement.SetAttributeValue(Language, node.Language);
        targetElement.SetAttributeValue(DashesCount, node.DashesCount);
        targetElement.SetAttributeValue(LeadingSpaces, node.LeadingSpaces);
        targetElement.Value = node.Content;
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, FrontMatterMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithLanguage(element.Attribute(Language)?.Value ?? string.Empty);
        targetNode.WithContent(element.Value);
        targetNode.WithDashesCount(int.Parse(element.Attribute(DashesCount)?.Value ?? "0"));
        targetNode.WithLeadingSpaces(int.Parse(element.Attribute(LeadingSpaces)?.Value ?? "0"));
    }
}
