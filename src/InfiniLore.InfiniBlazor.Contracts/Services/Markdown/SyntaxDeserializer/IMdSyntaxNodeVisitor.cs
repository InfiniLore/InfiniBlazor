// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxDeserializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNodeVisitor {
    void HandleOpenTag(IMdSyntaxNode node, StringBuilder builder);
    void HandleContent(IMdSyntaxNode node, StringBuilder builder);
    void HandleCloseTag(IMdSyntaxNode node, StringBuilder builder);
}
