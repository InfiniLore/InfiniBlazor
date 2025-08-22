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

    protected override void Deserialize(BlockQuoteMdSyntaxNode node, StringBuilder builder) {
        ReadOnlySpan<IMdSyntaxNode> span = node.GetChildrenSpan();
        if (span.Length == 0) return;
        
        builder.Append('>');
        if (span[0] is BlockQuoteMdSyntaxNode or CalloutMdSyntaxNode) builder.Append(' ', node.LeadingSpaces);
        else builder.Append(' ');

        for (int index = 0; index < span.Length; index++) {
            if (index > 0) {
                builder.Append('>');
                if (span[index] is not EmptyLineMdSyntaxNode) builder.Append(' ', node.LeadingSpaces);
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

        if (node.TryGetNextSibling(out IMdSyntaxNode? syntaxNode) && syntaxNode.Type != typeof(EmptyLineMdSyntaxNode)) builder.Append('\n');
    }
}
