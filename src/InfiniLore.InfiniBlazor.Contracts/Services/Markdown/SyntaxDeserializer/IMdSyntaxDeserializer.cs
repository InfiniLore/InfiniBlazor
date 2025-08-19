// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxDeserializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxDeserializer {
    MarkupString ConvertToMarkupString(IMdSyntaxTree tree);
    string ConvertToString(IMdSyntaxTree tree);
}
