// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ContentHtmlXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<ContentHtmlMdSyntaxNode> {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(ContentHtmlMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.Value = node.ContentHtml;
    }

    protected override void DeserializeDetails(XElement element, ContentHtmlMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.ContentHtml = element.Value;   
    }
}
