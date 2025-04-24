// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownSyntaxVisitor : IMarkdownSyntaxVisitor, IResettable {
    public int Depth { get; set; } = -1;
    public IMarkdownSyntaxNode Node { get; set; } = null!;
    
    public bool TryReset() {
        Depth = -1;
        Node = null!;
        return true;
    }
}