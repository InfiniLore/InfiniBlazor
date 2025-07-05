// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdSyntaxNodeConverter {
    StringBuilder Sb { set; }
    
    void HandleOpenTag(IMdSyntaxNode node);
    void HandleContent(IMdSyntaxNode node);
    void HandleCloseTag(IMdSyntaxNode node);
}
