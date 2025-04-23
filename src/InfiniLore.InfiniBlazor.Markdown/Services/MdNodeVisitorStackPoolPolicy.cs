// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.MdNodes;
using Microsoft.Extensions.ObjectPool;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNodeVisitorStackPoolPolicy(ObjectPool<MdNodeVisitor> visitorPool) : IPooledObjectPolicy<Stack<MdNodeVisitor>> {
    public Stack<MdNodeVisitor> Create() => new();
    public bool Return(Stack<MdNodeVisitor> obj) {
        while (obj.TryPop(out MdNodeVisitor? visitor)) {
            visitorPool.Return(visitor);
        }
        obj.Clear();
        return true;
    }
}
