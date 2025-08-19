// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml.NodeVisitors;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CalloutXmlMdSyntaxNodeVisitor : XmlMdSyntaxNodeVisitor<CalloutMdSyntaxNode> {
    private const string CalloutType = nameof(CalloutMdSyntaxNode.CalloutType);
    private const string CollapsedState = nameof(CalloutMdSyntaxNode.CollapsedState);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void SerializeDetails(CalloutMdSyntaxNode node, XElement targetElement) {
        base.SerializeDetails(node, targetElement);
        targetElement.SetAttributeValue(CalloutType, node.CalloutType);
        targetElement.SetAttributeValue(CollapsedState, node.CollapsedState);
    }

    protected override void DeserializeDetails(XElement element, CalloutMdSyntaxNode targetNode) {
        base.DeserializeDetails(element, targetNode);
        targetNode.CalloutType = element.Attribute(CalloutType)?.Value ?? string.Empty;
        targetNode.CollapsedState = (CalloutMdSyntaxNode.CollapseStateOptions)int.Parse(element.Attribute(CollapsedState)?.Value ?? "0");   
    }
}
