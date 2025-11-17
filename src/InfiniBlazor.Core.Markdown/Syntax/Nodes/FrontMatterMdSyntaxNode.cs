// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class FrontMatterMdSyntaxNode() : MdSyntaxNode<FrontMatterMdSyntaxNode>(initialChildCount:0) {
    public string Content { get; private set; } = string.Empty;
    public string Language { get; private set; } = string.Empty;
    public int DashesCount { get; private set; } = DashesCountValue;
    public int LeadingSpaces { get; private set; } = LeadingSpacesValue;

    private const int DashesCountValue = 3;
    private const int LeadingSpacesValue = 0;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public FrontMatterMdSyntaxNode WithContent(string content) {
        Content = content;
        return this;
    }
    
    public FrontMatterMdSyntaxNode WithLanguage(string language) {
        Language = language;
        return this;
    }
    
    public FrontMatterMdSyntaxNode WithDashesCount(int dashesCount) {
        DashesCount = dashesCount;
        return this;
    }
    
    public FrontMatterMdSyntaxNode WithLeadingSpaces(int leadingSpaces) {
        LeadingSpaces = leadingSpaces;
        return this;
    }  
    
    public override bool TryReset() {
        Content = string.Empty;
        Language = string.Empty;
        DashesCount = DashesCountValue;
        LeadingSpaces = LeadingSpacesValue;
        return base.TryReset();
    }

    protected override bool Equals(FrontMatterMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.Ordinal.Equals(Content, other.Content)
            && StringComparer.Ordinal.Equals(Language, other.Language);
}
