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
    protected override void DeserializeDetails(CodeInlineMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        AddXmlPreserveSpace(targetElement);
        targetElement.SetAttributeValue(nameof(CodeInlineMdSyntaxNode.BackTickCount), node.BackTickCount);
        targetElement.Value = node.OriginalContentCode;
    }

    protected override void SerializeDetails(XElement element, CodeInlineMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.BackTickCount = int.Parse(element.Attribute(nameof(CodeInlineMdSyntaxNode.BackTickCount))?.Value ?? "1");
        targetNode.OriginalContentCode = element.Value;   
    }
}
