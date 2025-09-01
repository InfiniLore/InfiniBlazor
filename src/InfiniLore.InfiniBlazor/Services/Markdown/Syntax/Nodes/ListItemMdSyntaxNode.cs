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
    public int LeadingSpaces { get; private set; }
    public int CheckLeadingSpaces { get; private set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ListItemMdSyntaxNode WithLeadingSpaces(int leadingSpaces) {
        LeadingSpaces = leadingSpaces;
        return this;
    }
    
    public ListItemMdSyntaxNode WithCheckLeadingSpaces(int checkLeadingSpaces) {
        CheckLeadingSpaces = checkLeadingSpaces;
        return this;
    }
    
    public ListItemMdSyntaxNode WithCheckMarker(string checkMarker = "x") {
        OriginalCheckMarker = checkMarker;
        return this;
    }
    
    public override bool TryReset() {
        IsCheckable = false;
        Index = string.Empty;
        OriginalCheckMarker = string.Empty;
        LeadingSpaces = 0;
        CheckLeadingSpaces = 0;
        return base.TryReset();
    }
    
    public override bool Equals(ListItemMdSyntaxNode? other) => base.Equals(other)
        && IsCheckable == other.IsCheckable
        && Index == other.Index
        && OriginalCheckMarker == other.OriginalCheckMarker;
}
