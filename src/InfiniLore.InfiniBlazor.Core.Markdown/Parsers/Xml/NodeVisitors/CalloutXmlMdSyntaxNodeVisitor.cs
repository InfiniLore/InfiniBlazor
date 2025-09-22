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
    private const string LeadingSpaces = nameof(CalloutMdSyntaxNode.LeadingSpaces);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void DeserializeDetails(CalloutMdSyntaxNode node, XElement targetElement) {
        base.DeserializeDetails(node, targetElement);
        
        targetElement.SetAttributeValue(CalloutType, node.CalloutType);
        targetElement.SetAttributeValue(CollapsedState, node.CollapsedState);
        targetElement.SetAttributeValue(LeadingSpaces, node.LeadingSpaces);
    }

    protected override void SerializeDetails(IMdSyntaxTree tree, XElement element, CalloutMdSyntaxNode targetNode) {
        base.SerializeDetails(tree, element, targetNode);
        
        targetNode.WithLeadingSpaces(int.Parse(element.Attribute(LeadingSpaces)?.Value ?? "0"));
        targetNode.WithCalloutType(element.Attribute(CalloutType)?.Value ?? string.Empty);
        
        if (Enum.TryParse(element.Attribute(CollapsedState)?.Value, out CalloutMdSyntaxNode.CollapseStateOptions value)) {
            targetNode.WithCollapseState(value);
        } 
    }
}
