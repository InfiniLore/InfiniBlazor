// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using System.Collections.Concurrent;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class CalloutSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<CalloutMdSyntaxNode> {
    private static ConcurrentDictionary<int, string> LeadingSpacesCache { get; } = new() {
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

    protected override void Deserialize(CalloutMdSyntaxNode node, StringBuilder builder) {
        builder.Append(">[!");
        builder.Append(node.CalloutType);
        if (node.TryGetModifier(out IMdSyntaxNodeModifier? modifier)) {
            builder.Append(modifier.OriginalInputSpan);
        }

        builder.Append(']');

        // Title does not contain any multiline structure, so we can deserialize it directly
        if (node.TryGetTitleNode(out CalloutTitleMdSyntaxNode? titleNode)) {
            builder.Append(' ');
            foreach (IMdSyntaxNode child in titleNode.GetChildrenSpan()) {
                if (!Deserializer.TryGetNodeDeserializer(child, out IMdStringMdSyntaxNodeDeserializer? deserializer)) continue;

                deserializer.Deserialize(child, builder);
            }
        }

        // Body contains multiline structure, so we need to deserialize it separately
        if (!node.TryGetBodyNode(out CalloutBodyMdSyntaxNode? bodyNode)) return;

        ReadOnlySpan<IMdSyntaxNode> span = bodyNode.GetChildrenSpan();
        if (span.Length == 0) return;

        StringBuilder contentBuilder = GlobalPools.StringBuilder.Get();
        try {
            // First, deserialize all children to get the raw content
            foreach (IMdSyntaxNode child in span) {
                if (!Deserializer.TryGetNodeDeserializer(child, out IMdStringMdSyntaxNodeDeserializer? deserializer)) continue;

                deserializer.Deserialize(child, contentBuilder);
            }

            if (contentBuilder.Length == 0) return;

            // Process content line by line without creating an array
            ReadOnlySpan<char> content = contentBuilder.ToString().AsSpan();
            int lineStart = 0;
            string leadingSpaces = LeadingSpacesCache.GetOrAdd(node.LeadingSpaces, static i => new string(' ', i));

            for (int i = 0; i <= content.Length; i++) {
                if (i != content.Length && content[i] != '\n') continue;

                ReadOnlySpan<char> line = content.Slice(lineStart, i - lineStart);
                builder.Append('\n');

                builder.Append('>');
                builder.Append(leadingSpaces);
                if (leadingSpaces.Length == 0) builder.Append(' ');
                builder.Append(line);

                // Move to the next line
                lineStart = i + 1;
            }
        }
        finally {
            GlobalPools.StringBuilder.Return(contentBuilder);
        }
    }
}
