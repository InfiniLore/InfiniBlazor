// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeBlockMdSyntaxNode() : MdSyntaxNode<CodeBlockMdSyntaxNode>(initialChildCount: 0) {
    public string Content { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public override bool TryReset() {
        Content = string.Empty;
        Language = string.Empty;
        return base.TryReset();
    }

    public override bool Equals(CodeBlockMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Content, other.Content)
            && StringComparer.InvariantCulture.Equals(Language, other.Language);
}
