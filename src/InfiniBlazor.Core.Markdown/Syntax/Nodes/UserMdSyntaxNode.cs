// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class UserMdSyntaxNode() : MdSyntaxNode<UserMdSyntaxNode>(initialChildCount:0) {
    public string Content { get; private set; } = string.Empty;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public UserMdSyntaxNode WithContent(string content) {
        Content = content;
        return this;
    }
    
    public override bool TryReset() {
        Content = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(UserMdSyntaxNode? other) 
        => base.Equals(other)
            && StringComparer.Ordinal.Equals(Content, other.Content);
}
