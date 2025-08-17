// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CalloutMdSyntaxNode : MdSyntaxNode<CalloutMdSyntaxNode> {
    public string? CalloutType { get; set; }
    public Option ExpandOption { get; set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void WithExpandOption(ReadOnlySpan<char> option) {
        if (option.IsEmpty) return;
        char c = option[0];
        ExpandOption = c switch {
            '+' => Option.Open,
            '-' => Option.Closed,
            _ => Option.None
        };
    }
    
    public override bool TryReset() {
        CalloutType = null;
        ExpandOption = Option.None;
        return base.TryReset();
    }

    public enum Option {
        None,
        Open,
        Closed
    }
}
