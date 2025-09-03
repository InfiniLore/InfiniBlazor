// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class UserMdSyntaxNode() : MdSyntaxNode<UserMdSyntaxNode>(initialChildCount:0) {
    public string UserName { get; set; } = string.Empty;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        UserName = string.Empty;
        return base.TryReset();
    }
    
    public override bool Equals(UserMdSyntaxNode? other) 
        => base.Equals(other)
            && UserName == other.UserName;
}
