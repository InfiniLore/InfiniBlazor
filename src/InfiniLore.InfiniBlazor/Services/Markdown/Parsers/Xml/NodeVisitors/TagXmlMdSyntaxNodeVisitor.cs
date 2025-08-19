// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TagXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<TagMdSyntaxNode> {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(TagMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.Value = node.ContentTag;
    }

    protected override void DeserializeDetails(XElement element, TagMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.ContentTag = element.Value;   
    }
}
