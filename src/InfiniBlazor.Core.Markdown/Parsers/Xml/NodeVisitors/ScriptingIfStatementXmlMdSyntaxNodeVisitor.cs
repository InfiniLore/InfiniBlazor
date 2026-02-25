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
public sealed class ScriptingIfStatementXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<ScriptingIfStatementSyntaxNode> {
    private const string ElseConditionIndex = nameof(ScriptingIfStatementSyntaxNode.ElseConditionIndex);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(ScriptingIfStatementSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        targetElement.SetAttributeValue(ElseConditionIndex, node.ElseConditionIndex);
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, ScriptingIfStatementSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        targetNode.WithElseConditionIndex(int.Parse(element.Attribute(ElseConditionIndex)?.Value ?? "-1"));
    }
}
