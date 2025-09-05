// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.HtmlString;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IHtmlStringMdSyntaxNodeVisitor {
    void HandleOpenTag(IMdSyntaxNode node, StringBuilder builder);
    void HandleContent(IMdSyntaxNode node, StringBuilder builder);
    void HandleCloseTag(IMdSyntaxNode node, StringBuilder builder);
}
