// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeInlineXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<CodeInlineMdSyntaxNode> {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(CodeInlineMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.Value = node.ContentCode;
    }

    protected override void DeserializeDetails(XElement element, CodeInlineMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.ContentCode = element.Value;   
    }
}
