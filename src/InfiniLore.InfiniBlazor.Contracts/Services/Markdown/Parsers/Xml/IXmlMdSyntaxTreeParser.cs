// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Xml.Linq;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Xml;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IXmlMdSyntaxTreeParser {
    XElement SerializeToElement(IMdSyntaxTree tree);
    Task SerializeToStreamAsync(Stream stream, IMdSyntaxTree tree, CancellationToken ct = default);
    Task SerializeToFileAsync(string filePath, IMdSyntaxTree tree, CancellationToken ct = default);
    IMdSyntaxTree DeserializeFromElement(XElement element);
    Task<IMdSyntaxTree> DeserializeFromStreamAsync(Stream stream, CancellationToken ct = default);
    Task<IMdSyntaxTree> DeserializeFromFileAsync(string filePath, CancellationToken ct = default);
}
