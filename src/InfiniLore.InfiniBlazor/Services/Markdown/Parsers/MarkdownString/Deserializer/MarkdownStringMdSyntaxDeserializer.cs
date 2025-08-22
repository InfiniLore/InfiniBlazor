// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxDeserializer>]
public class MarkdownStringMdSyntaxDeserializer(ILogger<MarkdownStringMdSyntaxDeserializer> logger) : IMarkdownStringMdSyntaxDeserializer {
    private readonly FrozenDictionary<Type, IMarkdownStringMdSyntaxNodeDeserializer> _deserializers = FrozenDictionary<Type, IMarkdownStringMdSyntaxNodeDeserializer>.Empty;

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
    
    public bool TryGetNodeDeserializer(IMdSyntaxNode node, [NotNullWhen(true)] out IMarkdownStringMdSyntaxNodeDeserializer? deserializer) {
        if (_deserializers.TryGetValue(node.Type, out deserializer)) return true;

        logger.Warning("No deserializer found for node type {NodeType}", node.Type);
        return false;
    }
}
