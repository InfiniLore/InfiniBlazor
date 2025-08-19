// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ContentMdSyntaxNode : MdSyntaxNode<ContentMdSyntaxNode> {
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
