// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMarkdownStringMdSyntaxDeserializer {
    string DeserializeToString(IMdSyntaxTree tree);
    bool TryGetNodeDeserializer(IMdSyntaxNode node, [NotNullWhen(true)] out IMarkdownStringMdSyntaxNodeDeserializer? deserializer);
}
