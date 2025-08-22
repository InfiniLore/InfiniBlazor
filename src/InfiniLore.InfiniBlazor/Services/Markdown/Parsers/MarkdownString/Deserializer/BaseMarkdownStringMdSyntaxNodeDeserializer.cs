// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class BaseMarkdownStringMdSyntaxNodeDeserializer<TNode> : IMarkdownStringMdSyntaxNodeDeserializer
    where TNode : IMdSyntaxNode
{
    public required IMarkdownStringMdSyntaxDeserializer Deserializer { get; init; } = null!;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Deserialize(IMdSyntaxNode node, StringBuilder builder) {
        if (node is not TNode typedNode) throw new ArgumentException($"Invalid node type of {node.GetType()} did adhere to {typeof(TNode)}");
        Deserialize(typedNode, builder);
    }
    
    protected virtual void DeserializeChildren(TNode node, StringBuilder builder) {
        foreach (IMdSyntaxNode child in node.GetChildrenSpan()) {
            if (!Deserializer.TryGetNodeDeserializer(child, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;
            deserializer.Deserialize(child, builder);
        }
    }
    
    protected abstract void Deserialize(TNode node, StringBuilder builder);
    
}
