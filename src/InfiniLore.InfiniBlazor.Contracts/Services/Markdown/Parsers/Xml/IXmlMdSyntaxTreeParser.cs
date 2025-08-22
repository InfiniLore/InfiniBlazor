// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IXmlMdSyntaxTreeParser {
    IMdSyntaxTree SerializeFromElement(XElement element);
    Task<IMdSyntaxTree> SerializeFromStreamAsync(Stream stream, CancellationToken ct = default);
    Task<IMdSyntaxTree> SerializeFromFileAsync(string filePath, CancellationToken ct = default);
    
    XElement DeserializeToElement(IMdSyntaxTree tree);
    Task DeserializeToStreamAsync(Stream stream, IMdSyntaxTree tree, CancellationToken ct = default);
    Task DeserializeToFileAsync(string filePath, IMdSyntaxTree tree, CancellationToken ct = default);
}
