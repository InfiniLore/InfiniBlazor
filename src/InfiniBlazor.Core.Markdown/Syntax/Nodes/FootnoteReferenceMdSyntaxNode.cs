// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class FootnoteReferenceMdSyntaxNode() : MdSyntaxNode<FootnoteReferenceMdSyntaxNode>(initialChildCount: 0) {
    public string Identifier { get; private set; } = string.Empty;
    
    public void WithIdentifier(string identifier) {
        Identifier = identifier;   
    }
    
    public override bool TryReset() {
        if (!base.TryReset()) return false;
        Identifier = string.Empty;
        return true;   
    }
}
