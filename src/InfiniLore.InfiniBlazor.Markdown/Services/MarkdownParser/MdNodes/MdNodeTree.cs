// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Pools;
using System.Collections;

namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdNodeTree : IMdNodeTree {
    public IMdNode RootNode { get; } = new MdNode();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<IMdNodeVisitor> GetEnumerator() {
        Stack<MdNodeVisitor> stack = MdNodeVisitorStackPool.Get();
        try {
            // Add Root children directly to avoid an extra if call during the while loop
            for (int i = RootNode.Children.Count - 1; i >= 0; i--) {
                if (RootNode.Children.ElementAtOrDefault(i) is not {} child) continue;

                MdNodeVisitor visitor = MdNodeVisitorPool.Get();
                visitor.Depth = 1;
                visitor.Node = child;
                stack.Push(visitor);
            }

            while (stack.TryPop(out MdNodeVisitor? visitor)) {
                int depth = visitor.Depth;
                IReadOnlyCollection<IMdNode> children = visitor.Node.Children;
            
                yield return visitor;
                MdNodeVisitorPool.Return(visitor);

                // Push children in reverse order so they're processed in the correct order when popped
                for (int i = children.Count - 1; i >= 0; i--) {
                    if (children.ElementAtOrDefault(i) is not {} child) continue;
                    
                    MdNodeVisitor childVisitor = MdNodeVisitorPool.Get();
                    childVisitor.Depth = depth + 1;
                    childVisitor.Node = child;
                    stack.Push(childVisitor);
                }
            }
        }
        finally {
            // Also handles MdNodeVisitorPool.Return(visitor) if the stack isnt empty yet
            MdNodeVisitorStackPool.Return(stack);
        }
    }
}
