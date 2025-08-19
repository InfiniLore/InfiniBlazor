// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxDeserializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxDeserializer {
    MarkupString DeserializeToMarkupString(IMdSyntaxTree tree);
    string DeserializeToString(IMdSyntaxTree tree);
}
