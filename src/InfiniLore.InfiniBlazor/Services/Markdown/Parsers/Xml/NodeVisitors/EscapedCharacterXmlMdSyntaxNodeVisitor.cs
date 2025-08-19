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
    protected override void SerializeDetails(EscapedCharacterMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.Value = node.ContentChar.ToString();
    }

    protected override void DeserializeDetails(XElement element, EscapedCharacterMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.ContentChar = element.Value.ElementAtOrDefault(0);   
    }
}
