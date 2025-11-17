// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;
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
        targetElement.Value = node.Content;
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, CodeBlockMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithLanguage(element.Attribute(Language)?.Value ?? string.Empty);
        targetNode.WithContent(element.Value);
    }
}
