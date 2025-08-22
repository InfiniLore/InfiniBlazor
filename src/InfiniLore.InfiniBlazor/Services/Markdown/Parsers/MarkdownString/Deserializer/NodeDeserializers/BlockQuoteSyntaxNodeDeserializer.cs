// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class BlockQuoteSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<BlockQuoteMdSyntaxNode> {

    protected override void DeserializeChildren(BlockQuoteMdSyntaxNode node, StringBuilder builder) {
        builder.Append("> ");
        
        StringBuilder localBuilder = GlobalPools.StringBuilder.Get();
        try {
            foreach (IMdSyntaxNode child in node.GetChildrenSpan()) {
                if (!Deserializer.TryGetNodeDeserializer(child, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;
                deserializer.Deserialize(child, localBuilder);
            }
            
            localBuilder.Replace("\n", "\n> "); // Prepend every line with "> "
            builder.Append(localBuilder);
        }
        finally {
            GlobalPools.StringBuilder.Return(localBuilder);
        }
    }
    
    protected override void Deserialize(BlockQuoteMdSyntaxNode node, StringBuilder builder) {
        DeserializeChildren(node, builder);
    }
}
