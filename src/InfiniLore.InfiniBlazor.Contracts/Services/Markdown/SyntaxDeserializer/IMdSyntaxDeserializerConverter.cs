// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxDeserializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxDeserializerConverter {
    void HandleOpenTag(IMdSyntaxNode node, StringBuilder builder);
    void HandleContent(IMdSyntaxNode node, StringBuilder builder);
    void HandleCloseTag(IMdSyntaxNode node, StringBuilder builder);
}
