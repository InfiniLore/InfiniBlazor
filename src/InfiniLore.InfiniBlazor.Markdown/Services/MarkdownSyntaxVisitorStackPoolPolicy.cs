// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownSyntaxVisitorStackPoolPolicy(ObjectPool<MarkdownSyntaxVisitor> visitorPool) : IPooledObjectPolicy<Stack<MarkdownSyntaxVisitor>> {
    public Stack<MarkdownSyntaxVisitor> Create() => new(255);
    public bool Return(Stack<MarkdownSyntaxVisitor> obj) {
        while (obj.TryPop(out MarkdownSyntaxVisitor? visitor)) {
            visitorPool.Return(visitor);
        }
        obj.Clear();
        return true;
    }
}
