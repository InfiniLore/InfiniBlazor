// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CalloutMdSyntaxNode : MdSyntaxNode<CalloutMdSyntaxNode> {
    public string? CalloutType { get; set; }
    public CollapseStateOptions CollapsedState { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetTitleNode([NotNullWhen(true)] out CalloutTitleMdSyntaxNode? titleNode) {
        titleNode = GetChildrenByType<CalloutTitleMdSyntaxNode>().FirstOrDefault();
        return titleNode is not null;
    }
    
    public bool TryGetBodyNode([NotNullWhen(true)] out CalloutBodyMdSyntaxNode? bodyNode) {
        bodyNode = GetChildrenByType<CalloutBodyMdSyntaxNode>().FirstOrDefault();
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
        CalloutType = null;
        CollapsedState = CollapseStateOptions.None;
        return base.TryReset();
    }

    public enum CollapseStateOptions {
        None,
        Open,
        Closed
    }
}
