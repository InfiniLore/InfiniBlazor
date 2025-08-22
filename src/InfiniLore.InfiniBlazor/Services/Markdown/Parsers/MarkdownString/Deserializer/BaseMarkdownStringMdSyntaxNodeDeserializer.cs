// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class BaseMarkdownStringMdSyntaxNodeDeserializer<TNode> : IMarkdownStringMdSyntaxNodeDeserializer
    where TNode : IMdSyntaxNode
{
    public IMarkdownStringMdSyntaxDeserializer Deserializer { get; internal set; } = null!;
    
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
    
    protected void AppendLastNewLineCorrectly(TNode node, StringBuilder builder) {
        if (node.TryGetNextSibling(out IMdSyntaxNode? syntaxNode) && syntaxNode is not EmptyLineMdSyntaxNode) builder.Append('\n');
    }
    
    protected abstract void Deserialize(TNode node, StringBuilder builder);
    
}
