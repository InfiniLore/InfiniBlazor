// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class FootnoteDescriptionMdSyntaxNode() : MdSyntaxNode<FootnoteDescriptionMdSyntaxNode>(initialChildCount:1) {
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
