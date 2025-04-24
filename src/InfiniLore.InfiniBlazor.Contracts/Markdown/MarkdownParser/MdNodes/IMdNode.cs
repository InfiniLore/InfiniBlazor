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
    IReadOnlyDictionary<string, string> Attributes { get; }
    IReadOnlySet<string> Classes { get; }
    IMdNode Parent { get; }
    
    ReadOnlySpan<T> GetChildrenSpan<T>(out int length) where T : IMdNode;

    IMdNode AddChildNode(MdElement element, string? content = null);

    IMdNode WithContent(string? content);
    IMdNode WithHtmlContent(string? content);
    IMdNode WithClass(string className);
    IMdNode WithAttribute(string key, string value);
}
