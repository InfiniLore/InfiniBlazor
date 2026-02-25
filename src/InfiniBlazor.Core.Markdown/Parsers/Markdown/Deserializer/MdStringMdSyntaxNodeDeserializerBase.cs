// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Collections.Concurrent;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class MdStringMdSyntaxNodeDeserializerBase<TNode> : IMdStringMdSyntaxNodeDeserializer
    where TNode : IMdSyntaxNode {
    public IMdStringMdSyntaxDeserializer Deserializer { get; internal init; } = null!;

    // ReSharper disable once StaticMemberInGenericType
    protected static ConcurrentDictionary<int, string> LeadingSpacesCache { get; } = new() {
        [0] = string.Empty,
        [1] = " ",
        [2] = "  ",
        [3] = "   ",
        [4] = "    ",
        [5] = "     ",
        [6] = "      ",
        [7] = "       ",
        [8] = "        ",
        [9] = "         "
    };
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Deserialize(IMdSyntaxNode node, StringBuilder builder) {
        if (node is not TNode typedNode) throw new ArgumentException($"Invalid node type of {node.GetType()} did adhere to {typeof(TNode)}");

        Deserialize(typedNode, builder);
    }

    protected void DeserializeChildren(TNode node, StringBuilder builder) {
        foreach (IMdSyntaxNode child in node.GetChildrenSpan()) {
            if (!Deserializer.TryGetNodeDeserializer(child, out IMdStringMdSyntaxNodeDeserializer? deserializer)) continue;

            deserializer.Deserialize(child, builder);
        }
    }

    protected abstract void Deserialize(TNode node, StringBuilder builder);
}
