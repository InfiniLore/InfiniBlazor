// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeBlockMdSyntaxNode() : MdSyntaxNode<CodeBlockMdSyntaxNode>(initialChildCount: 0) {
    public string Content { get; private set; } = string.Empty;
    public string Language { get; private set; } = string.Empty;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public CodeBlockMdSyntaxNode WithContent(string content) {
        Content = content;
        return this;
    }
    
    public CodeBlockMdSyntaxNode WithLanguage(string language) {
        Language = language;
        return this;
    }
    
    public override bool TryReset() {
        Content = string.Empty;
        Language = string.Empty;
        return base.TryReset();
    }

    protected override bool Equals(CodeBlockMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Content, other.Content)
            && StringComparer.InvariantCulture.Equals(Language, other.Language);
}
