// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HorizontalRuleMdSyntaxNode : EmptyMdSyntaxNode<HorizontalRuleMdSyntaxNode> {
    public string Identifier { get; set; } = string.Empty;

    public override bool TryReset() {
        Identifier = string.Empty;
        return base.TryReset();
    }
}
