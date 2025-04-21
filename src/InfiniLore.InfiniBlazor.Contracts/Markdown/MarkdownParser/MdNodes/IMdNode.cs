// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IMdNode {
    MdElement Element { get; }
    string? Content { get; }

    List<IMdNode> Children { get; }
    IMdNode Parent { get; }
    
    IMdNode AddChildNode(MdElement element);
    
    IMdNode WithContent(string content);
    IMdNode WithHtmlContent(string content);
    IMdNode WithClass(string className);
    IMdNode WithAttribute(string key, string value);
}
