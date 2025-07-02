// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ContentMdSyntaxNode : MdSyntaxNode<ContentMdSyntaxNode> {
    public string Content { get; set; } = string.Empty;
    protected override IMdSyntaxNode[] ChildNodes { get; set; } = GetInitialChildNodes(0); // Will never have children so don't initialize

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Content = string.Empty;
        return base.TryReset();
    }
}
