// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdStringMdSyntaxDeserializer {
    bool TryGetNodeDeserializer(IMdSyntaxNode node, [NotNullWhen(true)] out IMdStringMdSyntaxNodeDeserializer? deserializer);
    
    string DeserializeToString(IMdSyntaxTree tree);
}
