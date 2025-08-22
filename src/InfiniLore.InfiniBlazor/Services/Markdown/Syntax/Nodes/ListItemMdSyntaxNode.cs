// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListItemMdSyntaxNode : MdSyntaxNode<ListItemMdSyntaxNode> {
    public bool IsCheckable { get; set; }
    public bool IsChecked => OriginalCheckMarker.ToLowerInvariant().ElementAtOrDefault(0) == 'x';
    public string Index { get; set; } = string.Empty;
    public string OriginalCheckMarker { get; set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        IsCheckable = false;
        Index = string.Empty;
        OriginalCheckMarker = string.Empty;
        return base.TryReset();
    }
}
