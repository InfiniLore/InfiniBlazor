// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class BlockQuoteXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<BlockQuoteMdSyntaxNode> {
    private const string LeadingSpaces = nameof(BlockQuoteMdSyntaxNode.LeadingSpaces);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(BlockQuoteMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(LeadingSpaces, node.LeadingSpaces);
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, BlockQuoteMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithLeadingSpaces(int.Parse(element.Attribute(LeadingSpaces)?.Value ?? "0"));
    }
}
