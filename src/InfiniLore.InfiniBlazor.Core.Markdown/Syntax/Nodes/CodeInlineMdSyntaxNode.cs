// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CodeInlineMdSyntaxNode() : MdSyntaxNode<CodeInlineMdSyntaxNode>(initialChildCount: 0) {
    public string Content { get; private set; } = string.Empty;
    public int BackTickCount { get; private set; }


    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public CodeInlineMdSyntaxNode WithContent(string content) {
        Content = content;
        return this;
    }
    
    public CodeInlineMdSyntaxNode WithBackTickCount(int backTickCount) {
        BackTickCount = Math.Max(1, backTickCount);
        return this;   
    }
    
    public bool TryGetContentWithoutEscapedBackTicks(Span<char> destination, out int resultLength) {
        if (destination.Length < Content.Length) {
            resultLength = -1;
            return false;
        }
        ReadOnlySpan<char> originalSpan = Content.AsSpan();
        
        int destinationIndex = 0;
        for (int i = 0; i < originalSpan.Length; i++) {
            char c = originalSpan[i];
            if (i + 1 <= originalSpan.Length - 1 
                && c == '\\'
                && originalSpan[i + 1] == '`'
                && originalSpan.IsEscapedCharacterAtIndex(i + 1)) {
                continue;
            }
            destination[destinationIndex++] = c;
        }
        
        resultLength = destinationIndex;
        return true;
    }
    
    public override bool TryReset() {
        Content = string.Empty;
        BackTickCount = 0;
        return base.TryReset();
    }

    protected override bool Equals(CodeInlineMdSyntaxNode? other)
        => base.Equals(other)
            && StringComparer.InvariantCulture.Equals(Content, other.Content)
            && BackTickCount == other.BackTickCount;
}
