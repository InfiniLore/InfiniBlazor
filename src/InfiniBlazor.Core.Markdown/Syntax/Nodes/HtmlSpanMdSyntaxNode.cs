// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class HtmlSpanMdSyntaxNode : MdSyntaxNode<HtmlSpanMdSyntaxNode> {
    public string Attributes { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public HtmlSpanMdSyntaxNode WithAttributes(string attributes) {
        Attributes = attributes;
        return this;
    }
    
    public override bool TryReset() {
        Attributes = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(HtmlSpanMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.Ordinal.Equals(Attributes, other.Attributes);
}
