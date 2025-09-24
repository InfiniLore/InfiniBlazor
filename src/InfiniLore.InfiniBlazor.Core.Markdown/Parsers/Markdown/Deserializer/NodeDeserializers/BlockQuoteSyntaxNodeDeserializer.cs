// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using System.Collections.Concurrent;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class BlockQuoteSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<BlockQuoteMdSyntaxNode> {
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

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void Deserialize(BlockQuoteMdSyntaxNode node, StringBuilder builder) {
        StringBuilder contentBuilder = GlobalPools.StringBuilder.Get();
        try {
            if (node.ChildCount == 0) {
                builder.Append('>');
                builder.Append(' ');
                return;           
            }
            
            // First, deserialize all children to get the raw content
            DeserializeChildren(node, contentBuilder);

            if (contentBuilder.Length == 0) return;

            // Process content line by line without creating an array
            ReadOnlySpan<char> content = contentBuilder.ToString().AsSpan();
            int lineStart = 0;
            bool isFirstLine = true;
            string leadingSpaces = LeadingSpacesCache.GetOrAdd(Math.Max(node.LeadingSpaces, 0), static i => new string(' ', i));

            for (int i = 0; i <= content.Length; i++) {
                if (i != content.Length && content[i] != '\n') continue;
                
                ReadOnlySpan<char> line = content.Slice(lineStart, i - lineStart);
                if (!isFirstLine) builder.Append('\n');
                builder.Append('>');
                builder.Append(leadingSpaces);

                if (line.IsEmpty) builder.Append(' ');
                else builder.Append(line);

                // Move to the next line
                lineStart = i+1;
                isFirstLine = false;
            }
        }
        finally {
            GlobalPools.StringBuilder.Return(contentBuilder);
        }
    }
}
