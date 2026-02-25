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
public sealed class ScriptingExpressionXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<ScriptingExpressionSyntaxNode> {
    private const string FullStatement = nameof(ScriptingExpressionSyntaxNode.FullStatement);
    private const string ExpressionStart = nameof(ScriptingExpressionSyntaxNode.ExpressionStart);
    private const string ExpressionLength = nameof(ScriptingExpressionSyntaxNode.ExpressionLength);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ScriptingExpressionSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(FullStatement, node.FullStatement);
        targetElement.SetAttributeValue(ExpressionStart, node.ExpressionStart);
        targetElement.SetAttributeValue(ExpressionLength, node.ExpressionLength);
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, ScriptingExpressionSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        
        string fullStatement = element.Attribute(FullStatement)?.Value ?? string.Empty;
        int expressionStart = int.Parse(element.Attribute(ExpressionStart)?.Value ?? "0");
        int expressionLength = int.Parse(element.Attribute(ExpressionLength)?.Value ?? "0");
        
        targetNode.WithExpression(fullStatement, expressionStart, expressionLength);
    }
}
