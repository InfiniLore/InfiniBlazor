// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
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
        var stack = new Stack<(IMdNode Node, int Depth)>();
        stack.Push((RootNode, 0));

        while (stack.TryPop(out (IMdNode currentNode, int depth) box)) {
            int depth = box.depth;
            IMdNode currentNode = box.currentNode;
            
            if (currentNode.Element is not MdElement.Undefined) yield return new MdNodeVisitor(depth, currentNode);

            // Push children in reverse order so they're processed in the correct order when popped
            for (int i = currentNode.Children.Count - 1; i >= 0; i--) {
                if (currentNode.Children.ElementAtOrDefault(i) is not {} child) continue;
                stack.Push((child, depth + 1));
            }
        }
    }
}

public record struct MdNodeVisitor(int Depth, IMdNode CurrentNode) : IMdNodeVisitor;
