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
public sealed class ListOrderedSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<ListOrderedMdSyntaxNode> {
    protected override void Deserialize(ListOrderedMdSyntaxNode node, StringBuilder builder) {
        foreach (IMdSyntaxNode child in node.GetChildrenSpan()) {
            if (!Deserializer.TryGetNodeDeserializer(child, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;
            StringBuilder localBuilder = GlobalPools.StringBuilder.Get();
            try { 
                deserializer.Deserialize(child, localBuilder);
                
                localBuilder.Replace("\n", node.LeadingSpaces > 0 
                    ? $"\n{new string(' ', node.LeadingSpaces)}" 
                    : "\n"
                );
                builder.Append(localBuilder);
            }
            finally {
                GlobalPools.StringBuilder.Return(localBuilder);
            }
            if (child.HasNextSibling()) builder.Append('\n');
        }
        
        AppendLastNewLineCorrectly(node, builder);
    }
}
