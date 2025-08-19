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
    protected override void SerializeDetails(CodeBlockMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.SetAttributeValue(Language, node.Language);
        targetElement.Value = node.ContentCode;
    }

    protected override void DeserializeDetails(XElement element, CodeBlockMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.Language = element.Attribute(Language)?.Value ?? string.Empty;
        targetNode.ContentCode = element.Value;   
    }
}
