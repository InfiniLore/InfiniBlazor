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
            ReadOnlySpan<IMdSyntaxNode> span = bodyNode.GetChildrenSpan();
            if (span.Length == 0) return;
            builder.Append('\n');
            builder.Append('>');
            if (span[0] is BlockQuoteMdSyntaxNode or CalloutMdSyntaxNode) builder.Append(' ', node.LeadingSpaces);
            else builder.Append(' ');

            for (int index = 0; index < span.Length; index++) {
                if (index > 0) {
                    builder.Append('>');
                    if (span[index] is not NewLineMdSyntaxNode) builder.Append(' ', node.LeadingSpaces);
                }

                IMdSyntaxNode child = span[index];
                if (!Deserializer.TryGetNodeDeserializer(child, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;

                StringBuilder localBuilder = GlobalPools.StringBuilder.Get();
                try {
                    deserializer.Deserialize(child, localBuilder);
                    if (child is CodeBlockMdSyntaxNode or ListUnOrderedMdSyntaxNode or ListOrderedMdSyntaxNode or TableMdSyntaxNode or BlockQuoteMdSyntaxNode or CalloutMdSyntaxNode) {
                        // Prepend every line with "> "
                        localBuilder.Replace("\n", node.LeadingSpaces > 0 
                            ? $"\n>{new string(' ', node.LeadingSpaces)}" 
                            : "\n>"
                        );
                    }

                    builder.Append(localBuilder);
                }
                finally {
                    GlobalPools.StringBuilder.Return(localBuilder);
                }
            }
        }
    }
}
