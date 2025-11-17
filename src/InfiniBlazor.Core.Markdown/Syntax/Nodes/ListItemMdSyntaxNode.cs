// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

namespace InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListItemMdSyntaxNode : MdSyntaxNode<ListItemMdSyntaxNode> {
    public bool IsCheckable => OriginalCheckMarker.IsNotNullOrEmpty();
    public bool IsInapplicable => OriginalCheckMarker.ElementAtOrDefault(0) == '~';
    public bool IsChecked => OriginalCheckMarker.ToLowerInvariant().ElementAtOrDefault(0) == 'x';
    public string Index { get; private set; } = string.Empty;
    public string OriginalCheckMarker { get; private set; } = string.Empty;
    public int LeadingSpaces { get; private set; }
    public int CheckLeadingSpaces { get; private set; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ListItemMdSyntaxNode WithIndex(string index) {
        Index = index;
        return this;   
    }

    public ListItemMdSyntaxNode WithCheckMarker(string checkMarker) {
        OriginalCheckMarker = checkMarker;
        return this;
    }
    
    public ListItemMdSyntaxNode WithLeadingSpaces(int leadingSpaces) {
        LeadingSpaces = Math.Max(0, leadingSpaces);
        return this;
    }

    public ListItemMdSyntaxNode WithCheckLeadingSpaces(int checkLeadingSpaces) {
        CheckLeadingSpaces = Math.Max(0, checkLeadingSpaces);
        return this;
    }

    public override bool TryReset() {
        Index = string.Empty;
        OriginalCheckMarker = string.Empty;
        LeadingSpaces = 0;
        CheckLeadingSpaces = 0;
        return base.TryReset();
    }

    protected override bool Equals(ListItemMdSyntaxNode? other)
        => base.Equals(other)
            && IsCheckable == other.IsCheckable
            && StringComparer.Ordinal.Equals(Index, other.Index)
            && StringComparer.Ordinal.Equals(OriginalCheckMarker, other.OriginalCheckMarker)
            && LeadingSpaces == other.LeadingSpaces
            && CheckLeadingSpaces == other.CheckLeadingSpaces;
}
