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
        bool justPrefixed = true;

        for (int index = 0; index < span.Length; index++) {
            IMdSyntaxNode child = span[index];
            if (index > 0) {
                // Ensure separation between previous child content and the next blockquote prefix
                if (builder.Length > 0 && builder[^1] != '\n') {
                    builder.Append('\n');
                }

                // Always emit a blockquote prefix for the next child
                builder.Append('>');
                if (child is not NewLineMdSyntaxNode) builder.Append(' ', node.LeadingSpaces);
                justPrefixed = true;
            }

            if (!Deserializer.TryGetNodeDeserializer(child, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;

            StringBuilder localBuilder = GlobalPools.StringBuilder.Get();
            try {
                deserializer.Deserialize(child, localBuilder);

                // If we just wrote a blockquote prefix for this child and the child content starts with a newline,
                // remove that leading newline (supports both "\n" and "\r\n") to avoid an empty quoted line.
                if (justPrefixed && localBuilder.Length > 0 && localBuilder[0] == '\n') localBuilder.Remove(0, 1);

                if (child is CodeBlockMdSyntaxNode or ListUnOrderedMdSyntaxNode or ListOrderedMdSyntaxNode or TableMdSyntaxNode or BlockQuoteMdSyntaxNode or CalloutMdSyntaxNode) {
                    // For multi-line structures, we need to handle internal newlines only
                    // The replacement should not affect newlines that will be handled by the blockquote structure itself
                    string content = localBuilder.ToString();

                    // Split the content into lines
                    string[] lines = content.Split('\n');
                    localBuilder.Clear();

                    for (int i = 0; i < lines.Length; i++) {
                        if (i > 0) {
                            // Add newline and blockquote prefix for internal lines
                            localBuilder.Append('\n');
                            localBuilder.Append('>');
                            if (node.LeadingSpaces > 0) localBuilder.Append(' ', node.LeadingSpaces);
                        }

                        localBuilder.Append(lines[i]);
                    }
                }

                builder.Append(localBuilder);
            }
            finally {
                GlobalPools.StringBuilder.Return(localBuilder);
            }

            // After appending this child's content, we're no longer immediately after a prefix.
            justPrefixed = false;
        }

        AppendLastNewLineCorrectly(node, builder);
    }
}
