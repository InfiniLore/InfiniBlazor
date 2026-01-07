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
public sealed class FootnoteDescriptionXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<FootnoteDescriptionMdSyntaxNode> {
    private const string Identifier = nameof(FootnoteDescriptionMdSyntaxNode.Identifier);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(FootnoteDescriptionMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Identifier, node.Identifier);
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, FootnoteDescriptionMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithIdentifier(element.Attribute(Identifier)?.Value ?? string.Empty);
    }
}
