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
    private const string BackTickCount = nameof(CodeInlineMdSyntaxNode.BackTickCount);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(CodeInlineMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        AddXmlPreserveSpace(targetElement);
        targetElement.SetAttributeValue(BackTickCount, node.BackTickCount);
        if (node.Content.IsNotNullOrWhiteSpace()) targetElement.Value = node.Content;
    }

    protected override void SerializeDetails(XElement element, CodeInlineMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.BackTickCount = int.Parse(element.Attribute(BackTickCount)?.Value ?? "1");
        targetNode.Content = element.Value;   
    }
}
