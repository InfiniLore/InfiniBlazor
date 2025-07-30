// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNodeConverter {
    void HandleOpenTag(IMdSyntaxNode node, StringBuilder builder);
    void HandleContent(IMdSyntaxNode node, StringBuilder builder);
    void HandleCloseTag(IMdSyntaxNode node, StringBuilder builder);
}
