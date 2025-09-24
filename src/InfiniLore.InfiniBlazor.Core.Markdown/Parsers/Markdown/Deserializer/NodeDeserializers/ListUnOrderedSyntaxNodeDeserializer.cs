// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListUnOrderedSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<ListUnOrderedMdSyntaxNode> {
    protected override void Deserialize(ListUnOrderedMdSyntaxNode node, StringBuilder builder) {
        foreach (IMdSyntaxNode child in node.GetChildrenSpan()) {
            if (!Deserializer.TryGetNodeDeserializer(child, out IMdStringMdSyntaxNodeDeserializer? deserializer)) continue;
            StringBuilder localBuilder = GlobalPools.StringBuilder.Get();
            try { 
                deserializer.Deserialize(child, localBuilder);
                
                localBuilder.Replace("\n", node.LeadingSpaces > 0 
                    ? $"\n{new string(' ', node.LeadingSpaces)}" 
                    : "\n"
                );
                builder.Append(localBuilder);
                builder.Append('\n');
            }
            finally {
                GlobalPools.StringBuilder.Return(localBuilder);
            }
        }
        
        if (builder.Length == 0) return;
        builder.Remove(builder.Length - 1, 1);
    }
}
