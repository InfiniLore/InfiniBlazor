// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CalloutMdSyntaxNode() : MdSyntaxNode<CalloutMdSyntaxNode>(initialChildCount:2) {
    private const int TitleNodeIndex = 0;
    private const int BodyNodeIndex = 1;
    
    public string? CalloutType { get; private set; }
    public CollapseStateOptions CollapsedState { get; private set; }
    public int LeadingSpaces { get; private set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public CalloutMdSyntaxNode WithCalloutType(string calloutType) {
        CalloutType = calloutType;
        return this;
    }
    
    public CalloutMdSyntaxNode WithLeadingSpaces(int leadingSpaces) {
        LeadingSpaces = Math.Max(0, leadingSpaces);
        return this;
    }
    
    public CalloutMdSyntaxNode WithCollapseState(CollapseStateOptions collapseState) {
        CollapsedState = collapseState;
        return this;
    }
    
    public bool TrySetTitle(CalloutTitleMdSyntaxNode titleNode) 
        => TryAddChildNodeAtIndex(TitleNodeIndex, titleNode);

    public bool TrySetBody(CalloutBodyMdSyntaxNode bodyNode)
        => TryAddChildNodeAtIndex(BodyNodeIndex, bodyNode); 
    
    public bool TryGetTitleNode([NotNullWhen(true)] out CalloutTitleMdSyntaxNode? titleNode) {
        titleNode = null;
        if (ChildCount == 0) return false;
        titleNode = ChildNodes[TitleNodeIndex] as CalloutTitleMdSyntaxNode;
        return titleNode is not null;
    }
    
    public bool TryGetBodyNode([NotNullWhen(true)] out CalloutBodyMdSyntaxNode? bodyNode) {
        bodyNode = null;
        if (ChildCount == 0) return false;
        bodyNode = ChildNodes[BodyNodeIndex] as CalloutBodyMdSyntaxNode;
        return bodyNode is not null;
    }
    
    public void WithExpandOption(ReadOnlySpan<char> option) {
        if (option.IsEmpty) return;
        char c = option[0];
        CollapsedState = c switch {
            '+' => CollapseStateOptions.Open,
            '-' => CollapseStateOptions.Closed,
            _ => CollapseStateOptions.None
        };
    }
    
    public override bool TryReset() {
        LeadingSpaces = 0;
        CalloutType = null;
        CollapsedState = CollapseStateOptions.None;
        return base.TryReset();
    }

    protected override bool Equals(CalloutMdSyntaxNode? other)
        => base.Equals(other)
            && LeadingSpaces == other.LeadingSpaces
            && CollapsedState == other.CollapsedState
            && StringComparer.Ordinal.Equals(CalloutType, other.CalloutType);

    public enum CollapseStateOptions {
        None = 0,
        Open = 1,
        Closed = 2
    }
}
