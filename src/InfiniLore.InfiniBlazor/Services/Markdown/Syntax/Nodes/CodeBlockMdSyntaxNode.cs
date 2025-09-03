// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeBlockMdSyntaxNode() : MdSyntaxNode<CodeBlockMdSyntaxNode>(initialChildCount:0) {
    public string ContentCode { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        ContentCode = string.Empty;
        Language = string.Empty;
        return base.TryReset();
    }
    
    public override bool Equals(CodeBlockMdSyntaxNode? other) => base.Equals(other)
        && ContentCode == other.ContentCode
        && Language == other.Language;
}
