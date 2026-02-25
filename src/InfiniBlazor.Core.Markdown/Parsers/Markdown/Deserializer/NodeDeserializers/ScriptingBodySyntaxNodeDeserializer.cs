// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniBlazor.Pooling;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ScriptingBodySyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<ScriptingBodySyntaxNode> {

    protected override void Deserialize(ScriptingBodySyntaxNode node, StringBuilder builder) {
        if (node.ChildCount == 0) return;

        StringBuilder contentBuilder = GlobalPools.StringBuilder.Get();
        try {
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
                builder.Append(leadingSpaces);

                if (line.IsEmpty) builder.Append(' ');
                else builder.Append(line);

                // Move to the next line
                lineStart = i + 1;
                isFirstLine = false;
            }
        }
        finally {
            GlobalPools.StringBuilder.Return(contentBuilder);
        }
    }
}
