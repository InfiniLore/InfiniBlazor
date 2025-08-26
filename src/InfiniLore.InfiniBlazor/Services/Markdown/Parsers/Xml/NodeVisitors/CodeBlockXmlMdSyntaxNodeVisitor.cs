// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeBlockXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<CodeBlockMdSyntaxNode> {
    private const string Language = nameof(CodeBlockMdSyntaxNode.Language);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(CodeBlockMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        AddXmlPreserveSpace(targetElement);
        targetElement.SetAttributeValue(Language, node.Language);
        targetElement.Value = node.ContentCode;
    }

    protected override void SerializeDetails(XElement element, CodeBlockMdSyntaxNode targetNode) {
        base.SerializeDetails(element, targetNode);
        targetNode.Language = element.Attribute(Language)?.Value ?? string.Empty;
        targetNode.ContentCode = element.Value;   
    }
}
