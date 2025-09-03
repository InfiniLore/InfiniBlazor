// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeInlineMdSyntaxNode() : MdSyntaxNode<CodeInlineMdSyntaxNode>(initialChildCount:0) {
    public string ContentCode => OriginalContentCode.Replace("\\`", "`");
    public string OriginalContentCode { get; set; } = string.Empty;
    public int BackTickCount { get; set; }
    
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        OriginalContentCode = string.Empty;
        BackTickCount = 0;
        return base.TryReset();
    }

    public override bool Equals(CodeInlineMdSyntaxNode? other) => base.Equals(other)
        && OriginalContentCode == other.OriginalContentCode
        && BackTickCount == other.BackTickCount;
}
