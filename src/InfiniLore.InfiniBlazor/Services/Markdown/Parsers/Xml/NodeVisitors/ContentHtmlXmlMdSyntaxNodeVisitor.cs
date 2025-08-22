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
    protected override void DeserializeDetails(ContentHtmlMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.Value = node.ContentHtml;
    }

    protected override void SerializeDetails(XElement element, ContentHtmlMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.ContentHtml = element.Value;   
    }
}
