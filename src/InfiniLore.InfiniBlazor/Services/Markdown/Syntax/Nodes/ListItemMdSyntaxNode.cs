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
    
    public override bool Equals(ListItemMdSyntaxNode? other) => base.Equals(other)
        && IsCheckable == other.IsCheckable
        && Index == other.Index
        && OriginalCheckMarker == other.OriginalCheckMarker;
}
