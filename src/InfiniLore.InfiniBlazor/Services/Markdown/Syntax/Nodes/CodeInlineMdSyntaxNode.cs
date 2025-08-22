// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeInlineMdSyntaxNode : EmptyMdSyntaxNode<CodeInlineMdSyntaxNode> {
    public string ContentCode => OriginalContentCode.Replace("`", "\\`");
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
}
