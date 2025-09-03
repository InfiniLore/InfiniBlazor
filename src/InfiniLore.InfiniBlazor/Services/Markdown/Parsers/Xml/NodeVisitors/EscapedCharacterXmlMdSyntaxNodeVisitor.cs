// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EscapedCharacterXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<EscapedCharacterMdSyntaxNode> {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(EscapedCharacterMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        AddXmlPreserveSpace(targetElement);
        targetElement.Value = node.Content.ToString();
    }

    protected override void SerializeDetails(XElement element, EscapedCharacterMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.Content = element.Value.ElementAtOrDefault(0);   
    }
}
