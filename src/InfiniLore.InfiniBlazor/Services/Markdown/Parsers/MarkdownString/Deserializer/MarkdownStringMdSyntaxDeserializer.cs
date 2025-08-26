// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownStringMdSyntaxDeserializer(ILogger<MarkdownStringMdSyntaxDeserializer> logger) : IMarkdownStringMdSyntaxDeserializer {
    public FrozenDictionary<Type, IMarkdownStringMdSyntaxNodeDeserializer> Deserializers { get; internal set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string DeserializeToString(IMdSyntaxTree tree) {
        StringBuilder builder = GlobalPools.StringBuilder.Get();

        try {
            foreach (IMdSyntaxNode node in tree.VisitTopLevelNodes()) {
                if (!TryGetNodeDeserializer(node, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;
                deserializer.Deserialize(node, builder);
            }
            
            return builder.ToString();
        }
        finally {
            GlobalPools.StringBuilder.Return(builder);
        }
    }
    
    // ReSharper disable once ConvertIfStatementToReturnStatement
    public bool TryGetNodeDeserializer(IMdSyntaxNode node, [NotNullWhen(true)] out IMarkdownStringMdSyntaxNodeDeserializer? deserializer) {
        if (Deserializers.TryGetValue(node.Type, out deserializer)) return true;

        logger.Error("No deserializer found for node type {NodeType}", node.Type);
        throw new InvalidOperationException($"No deserializer found for node type {node.Type}"); // TODO dont treat as an exception
    }
}
