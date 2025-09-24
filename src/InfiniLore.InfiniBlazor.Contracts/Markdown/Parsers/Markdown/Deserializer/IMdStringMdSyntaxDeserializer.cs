// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Deserializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdStringMdSyntaxDeserializer {
    string DeserializeToString(IMdSyntaxTree tree);
    bool TryGetNodeDeserializer(IMdSyntaxNode node, [NotNullWhen(true)] out IMdStringMdSyntaxNodeDeserializer? deserializer);
}
