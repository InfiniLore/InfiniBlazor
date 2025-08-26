// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HtmlSpanMdSyntaxNode : MdSyntaxNode<HtmlSpanMdSyntaxNode> {
    public string TagValue { get; set; } = string.Empty;
    public string Attributes { get; set; } = string.Empty;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        TagValue = string.Empty;
        Attributes = string.Empty;
        return base.TryReset();
    }
    
    public override bool Equals(HtmlSpanMdSyntaxNode? other) => base.Equals(other)
        && TagValue == other.TagValue
        && Attributes == other.Attributes;
}
