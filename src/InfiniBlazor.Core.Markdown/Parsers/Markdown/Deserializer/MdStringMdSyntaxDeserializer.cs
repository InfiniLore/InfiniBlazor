// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Pooling;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdStringMdSyntaxDeserializer(ILogger<MdStringMdSyntaxDeserializer> logger) : IMdStringMdSyntaxDeserializer {
    public FrozenDictionary<Type, IMdStringMdSyntaxNodeDeserializer> Deserializers { get; internal set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string DeserializeToString(IMdSyntaxTree tree) {
        StringBuilder builder = GlobalPools.StringBuilder.Get();

        try {
            foreach (IMdSyntaxNode node in tree.VisitTopLevelNodes()) {
                if (!TryGetNodeDeserializer(node, out IMdStringMdSyntaxNodeDeserializer? deserializer)) continue;

                deserializer.Deserialize(node, builder);
            }

            return builder.ToString();
        }
        finally {
            GlobalPools.StringBuilder.Return(builder);
        }
    }

    public bool TryGetNodeDeserializer(IMdSyntaxNode node, [NotNullWhen(true)] out IMdStringMdSyntaxNodeDeserializer? deserializer) {
        if (Deserializers.TryGetValue(node.Type, out deserializer)) return true;

        logger.Error("No deserializer found for node type {NodeType}", node.Type);
        return false;
    }
}
