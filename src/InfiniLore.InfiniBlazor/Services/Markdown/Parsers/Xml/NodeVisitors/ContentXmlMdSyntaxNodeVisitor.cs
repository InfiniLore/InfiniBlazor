// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ContentXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<ContentMdSyntaxNode> {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(ContentMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.Value = node.Content;
    }

    protected override void DeserializeDetails(XElement element, ContentMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.Content = element.Value;   
    }
}
