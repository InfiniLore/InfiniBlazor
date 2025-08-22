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
public sealed class CalloutSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<CalloutMdSyntaxNode> {
    
    protected override void Deserialize(CalloutMdSyntaxNode node, StringBuilder builder) {
        builder.Append("> [!");
        builder.Append(node.CalloutType);
        if (node.TryGetModifier(out IMdSyntaxNodeModifier? modifier)) {
            builder.Append(modifier.OriginalInputSpan);
        }
        builder.Append(']');
        
        // Title does not contain any multiline structure, so we can deserialize it directly
        if (node.TryGetTitleNode(out CalloutTitleMdSyntaxNode? titleNode)) {
            builder.Append(' ');
            foreach (IMdSyntaxNode child in titleNode.GetChildrenSpan()) {
                if (!Deserializer.TryGetNodeDeserializer(child, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;
                deserializer.Deserialize(child, builder);
            }
        }

        
        // Body contains multiline structure, so we need to deserialize it separately
        if (node.TryGetBodyNode(out CalloutBodyMdSyntaxNode? bodyNode)) {
            ReadOnlySpan<IMdSyntaxNode> span = node.GetChildrenSpan();
            if (span.Length == 0) return;
            builder.Append('\n');
            builder.Append('>');
            builder.Append(' ');
        
            foreach (IMdSyntaxNode child in span) {
                if (!Deserializer.TryGetNodeDeserializer(child, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;

                StringBuilder localBuilder = GlobalPools.StringBuilder.Get();
                try {
                    deserializer.Deserialize(child, localBuilder);
                    localBuilder.Replace("\n", "\n> ");// Prepend every line with "> "
                    builder.Append(localBuilder);
                }
                finally {
                    GlobalPools.StringBuilder.Return(localBuilder);
                }
            }
        }
        
        if (node.HasNextSibling()) builder.Append('\n');
    }
}
